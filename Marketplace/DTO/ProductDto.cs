using data.model;

namespace Marketplace.DTO;

public class ProductDto
{
    private IConfiguration _configuration;
    
    public ProductDto(IConfiguration configuration)
    {
        
    }
    public ProductDto(ProductEntity productEntity,  IConfiguration configuration)
    {
        _configuration = configuration;
        var fileInfoOptions = new FileInfoOptions();
        configuration.GetSection(FileInfoOptions.File).Bind(fileInfoOptions);
        
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
        if (productEntity.PhotoId != null)
        {
            PhotoId = $"{fileInfoOptions.BaseUrl}/{fileInfoOptions.RequestPath}/{productEntity.Creator.Id}/{productEntity.PhotoId.Id}{productEntity.PhotoId.Extension}";
            
        }
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public int PartNumber { get; set; }
    public string? Description { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public Country Country { get; set; }
    public string? PhotoId { get; set; }
    public UserDto Owner { get; set; } = null!;
}