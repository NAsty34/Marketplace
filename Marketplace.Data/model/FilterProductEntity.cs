namespace data.model;

public class FilterProductEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? Country { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Depth { get; set; }
    public double? MinWeight { get; set; }
    public double? MaxWeight { get; set; }
}