namespace data.model;

public class FiltersShops
{
    public IEnumerable<Guid>? Id { get; set; }
    
    public string? Name { get; set; }
    public override string ToString()
    {
        return Id + " " + Name+" "+category+" ";
    }

    public bool? IsPublic { get; set; }
    public Guid? User { get; set; }
    public int? page { get; set; }
    public int? size { get; set; }
    public IEnumerable<Guid>? category { get; set; }
    public IEnumerable<Guid>? deliveries { get; set; }
    public IEnumerable<Guid>? payment { get; set; }
    public IEnumerable<Guid>? types { get; set; }
}

