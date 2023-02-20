using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("Feedback")]
public class Feedback:BaseEntity
{
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public Guid ShopId { get; set; }
    public virtual Shop? Shop { get; set; }
    public virtual User? Creator { get; set; }
}