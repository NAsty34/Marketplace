using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

public class ProductEntity:BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual CategoryEntity? Category { get; set; }

    public int PartNumber { get; set; }
    public string? Description { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public virtual UserEntity Creator { get; set; } = null!;
    public double Depth { get; set; }
    public CountryEntity Country { get; set; }

    public List<Guid>? PhotoId { get; set; } 

    public virtual IEnumerable<FileInfoEntity>? Photo { get; set; }
    public List<string>? UrlPhotos { get; set; }
}