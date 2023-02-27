using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("Feedback")]
public class FeedbackEntity:BaseEntity
{
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public Guid ShopId { get; set; }
    public virtual ShopEntity Shop { get; set; } = null!;
    public virtual UserEntity Creator { get; set; } = null!;
}