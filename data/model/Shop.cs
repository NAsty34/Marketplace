using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("Shop")]
public class Shop:BaseEntity
{
    public  string? Name { get; set; }
    public  string? Description { get; set; }
    public  string? Logo { get; set; }
    public  string Inn { get; set; }
    public bool isPublic { get; set; }
    public User Creator { get; set; }
    public List<User> Users { get; set; } = new();
}