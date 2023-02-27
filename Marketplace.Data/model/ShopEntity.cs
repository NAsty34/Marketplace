using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("Shop")]
public class ShopEntity:BaseEntity
{
    
    public  string Name { get; set; } = null!;
    public  string Description { get; set; } = null!;
    public Guid? LogoId { get; set; }
    
    [ForeignKey(nameof(LogoId))]
    public  virtual FileInfoEntity? Logo { get; set; }
    
    public  string Inn { get; set; } = null!;
    public bool IsPublic { get; set; }
    public virtual UserEntity Creator { get; set; } = null!;
    public virtual List<UserEntity> Users { get; set; } = new();
    public double MinPrice { get; set; }
    public virtual IEnumerable<ShopCategoryEntity> ShopCategory { get; set; } = null!;
    public virtual IEnumerable<ShopDeliveryEntity> ShopDeliveries { get; set; } = null!;
    public virtual IEnumerable<ShopPaymentEntity> ShopPayment { get; set; }= null!;
    public virtual IEnumerable<ShopTypesEntity> ShopTypes { get; set; } = null!;
}