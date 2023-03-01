using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("FavoriteShop")]
public class FavoriteShopsEntity
{
    
    public Guid ShopId { get; set; }
    [ForeignKey(nameof(ShopId))]
    public virtual ShopEntity Shop { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual UserEntity User { get; set; }
    
    
}