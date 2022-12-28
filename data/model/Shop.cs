namespace data.model;

public class Shop
{
    public  int Id { get; set; }
    public  string? Name { get; set; }
    public  string? Description { get; set; }
    public  string? Logo { get; set; }
    public  string? Inn { get; set; }
    public bool isPublic { get; set; }
    public bool isBlock { get; set; }
}