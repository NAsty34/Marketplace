namespace data.model;

public class FiltersShops
{
    public IEnumerable<Guid>? Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
  
    public bool? IsPublic { get; set; }
    public Guid? User { get; set; }
    public int? Page { get; set; }
    public int? Size { get; set; }
    public IEnumerable<Guid>? Category { get; set; }
    public IEnumerable<Guid>? Deliveries { get; set; }
    public IEnumerable<Guid>? Payment { get; set; }
    public IEnumerable<Guid>? Types { get; set; }
}

