using System.Text.Json.Serialization;
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
        Width = productEntity.Width;
        Weight = productEntity.Weight;
        CountryString = productEntity.Country.ToString();
        Depth = productEntity.Depth;
        Height = productEntity.Height;
        Owner = new UserDto(productEntity.Creator);
        IsBlock = !productEntity.IsActive;
        CategoryId = productEntity.CategoryId;
        Category = new ShortCategoryDto(productEntity.Category);
    }
    public Guid Id { get; set; }
    public bool IsBlock { get; set; }
    public string Name { get; set; } = null!;
    public ShortCategoryDto Category { get; set; }
    [JsonIgnore]
    public Guid CategoryId { get; set; }
    public int PartNumber { get; set; }
    public string? Description { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public string CountryString { get; set; }
    [JsonIgnore]
    public CountryEntity Country { get; set; }
    public string? Photo { get; set; }
    public UserDto Owner { get; set; } = null!;
}