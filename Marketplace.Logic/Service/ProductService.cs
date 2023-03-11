using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;

namespace logic.Service;

public class ProductService : IProductService
{
    private IProductRepository _productRepository;
    private CategoryRepository _categoryRepository;
    private CategoryService _categoryService;
    private ILogger<ProductEntity> _logger;
    private IUserRepository _userRepository;

    public ProductService(IProductRepository productRepository, CategoryRepository categoryRepository,
        ILogger<ProductEntity> logger, CategoryService categoryService, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _logger = logger;
        _categoryService = categoryService;
        _userRepository = userRepository;
    }

    public async Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity filterProductEntity, int? page,
        int? size)
    {
        return await _productRepository.GetProducts(filterProductEntity, page, size);
    }

    public async Task<ProductEntity> CreateProduct(ProductEntity productEntity)
    {
        var fromDb = await _productRepository.GetByCodEdit(productEntity.PartNumber);
        if (fromDb != null) return await EditProduct(productEntity, fromDb);

        await _productRepository.Create(productEntity);
        await _productRepository.Save();
        return productEntity;
    }

    public async Task<ProductEntity> EditProduct(ProductEntity productEntity)
    {
        var fromDb = await _productRepository.GetById(productEntity.Id);
        if (fromDb == null || !fromDb.IsActive)
        {
            throw new ProductNotFoundException();
        }

        return await EditProduct(productEntity, fromDb);
    }

    private async Task<ProductEntity> EditProduct(ProductEntity productEntity, ProductEntity fromDb)
    {
        var category = await _categoryRepository.GetById(productEntity.CategoryId);
        if (category == null || !category.IsActive) throw new CategoryNotFoundException();

        fromDb.Country = productEntity.Country;
        fromDb.Description = productEntity.Description;
        fromDb.Depth = productEntity.Depth;
        fromDb.Height = productEntity.Height;
        fromDb.Name = productEntity.Name;
        fromDb.Width = productEntity.Width;
        fromDb.Weight = productEntity.Weight;
        fromDb.CategoryId = productEntity.CategoryId;
        fromDb.PartNumber = productEntity.PartNumber;
        fromDb.Photo = productEntity.Photo;
        await _productRepository.Edit(fromDb);
        await _productRepository.Save();
        return fromDb;
    }

    public async Task<ProductEntity> IsActiveProduct(Guid id, bool value)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.IsActive = value;
        await _productRepository.Save();
        return product;
    }

    public async Task DeletedProduct(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null || !product.IsActive) throw new ProductNotFoundException();
        await _productRepository.Delete(product);
        await _productRepository.Save();
    }

    public async Task<IEnumerable<ProductEntity>> Upload(IFormFile productFile, Guid userId)
    {
        var dictionary = new Dictionary<int, ProductEntity>();
        var user = await _userRepository.GetById(userId);

        using (var stream = new MemoryStream())
        {
            await productFile.CopyToAsync(stream);

            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var col = 4;
                    var index = 1;
                    for (int i = 1; i <= 4; i++)
                    {
                        if (worksheet.Cells[row, i].Value == null ||
                            String.IsNullOrEmpty(worksheet.Cells[row, i].Value.ToString())) continue;

                        if (await _categoryRepository.GetByName(worksheet.Cells[row, i].Value.ToString()) == null)
                        {
                            await _categoryService.Create(new CategoryEntity
                            {
                                Parent = i == 1
                                    ? null
                                    : await _categoryRepository.GetByName(worksheet.Cells[row, i - 1].Value
                                        .ToString()),
                                Id = Guid.NewGuid(),
                                Name = worksheet.Cells[row, i].Value.ToString()
                            });
                        }
                        index = i;
                    }

                    List<string>? urls = null;
                    
                    if (worksheet.Cells[row, col + 9].Value != null)
                    {
                        if (!worksheet.Cells[row, col + 9].Value.ToString()!.Contains(';'))
                        {
                            urls = new List<string> { worksheet.Cells[row, col + 9].Value.ToString()! };
                        }

                        urls = worksheet.Cells[row, col + 9].Value.ToString()!.Replace(" ", "").Split(";").ToList();
                    }

                    var productEntity = new ProductEntity
                    {
                        Id = Guid.NewGuid(),
                        CategoryId =
                            (await _categoryRepository.GetByName(
                                worksheet.Cells[row, index].Value.ToString().Trim())).Id,
                        PartNumber = int.Parse(worksheet.Cells[row, col + 1].Value.ToString().Trim().ToLower()),
                        Name = worksheet.Cells[row, col + 2].Value.ToString().Trim().ToLower(),
                        Description = worksheet.Cells[row, col + 3].Value.ToString().Trim().ToLower(),
                        Weight = double.Parse(worksheet.Cells[row, col + 4].Value.ToString().Trim().ToLower()),
                        Width = double.Parse(worksheet.Cells[row, col + 5].Value.ToString().Trim().ToLower()),
                        Height = double.Parse(worksheet.Cells[row, col + 6].Value.ToString().Trim().ToLower()),
                        Depth = double.Parse(worksheet.Cells[row, col + 7].Value.ToString().Trim().ToLower()),
                        Country = Enum.Parse<CountryEntity>(worksheet.Cells[row, col + 8].Value.ToString().Trim()),
                        UrlPhotos = urls,
                        Creator = user
                    };
                    dictionary.Add(productEntity.PartNumber, productEntity);
                    if (dictionary.Count >= 20)
                    {
                        await SaveDictionary(dictionary, user);
                        dictionary.Clear();
                    }
                }
            }
            if (!dictionary.IsNullOrEmpty())
            {
                await SaveDictionary(dictionary, user);
            }
        }
        return dictionary.Values;
    }

    private async Task SaveDictionary(Dictionary<int, ProductEntity> dictionary, UserEntity? user)
    {
        var productsFromDb = await _productRepository.GetByPartNumber(dictionary.Keys);
        foreach (var prod in productsFromDb)
        {
            var product = dictionary.GetValueOrDefault(prod.PartNumber, null);
            if (product == null) continue;
            dictionary.Remove(product.PartNumber);
            prod.CategoryId = product.CategoryId;
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Weight = product.Weight;
            prod.Width = product.Width;
            prod.Height = product.Height;
            prod.Depth = product.Depth;
            prod.Country = product.Country;
            prod.UrlPhotos = product.UrlPhotos;
            prod.EditorId = user.Id;
        }

        await _productRepository.Edit(productsFromDb);
        await _productRepository.Create(dictionary.Values);
        await _productRepository.Save();
    }

    
}