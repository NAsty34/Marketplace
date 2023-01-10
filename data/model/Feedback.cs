using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("Feedback")]
public class Feedback:BaseEntity
{
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public int ShopId { get; set; }
    public Shop? Shop { get; set; }
    public User Creator { get; set; }
}