using data.model;
using Microsoft.Extensions.Options;

namespace Marketplace.DTO;

public class ProductDto
{
    private FileInfoOptions _options;
    
    public ProductDto()
    {
        
    }
    public ProductDto(ProductEntity productEntity)
    {
        Id = productEntity.Id;
        Name = productEntity.Name;
        Description = productEntity.Description;
        PartNumber = productEntity.PartNumber;
        CategoryId = productEntity.CategoryId;
        Width = productEntity.Width;
        Weight = productEntity.Weight;
        Country = productEntity.Country;
        Depth = productEntity.Depth;
        Height = productEntity.Height;
        Owner = new UserDto(productEntity.Creator);
        isBlock = !productEntity.IsActive;

    }
    public Guid Id { get; set; }
    public bool isBlock { get; set; }
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public int PartNumber { get; set; }
    public string? Description { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public CountryEntity Country { get; set; }
    public string? Photo { get; set; }
    public UserDto Owner { get; set; } = null!;
}