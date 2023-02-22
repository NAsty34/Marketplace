namespace data.model;

public class ProductEntity:BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public int PartNumber { get; set; }
    public string? Description { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public virtual User Creator { get; set; } = null!;
    public double Depth { get; set; }
    public Country Country { get; set; }
    public virtual FileInfoEntity? PhotoId { get; set; }
}