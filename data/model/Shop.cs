using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

[Table("Shop")]
public class Shop:BaseEntity
{
    
    public  string Name { get; set; } = null!;
    public  string Description { get; set; } = null!;
    public  virtual FileInfoEntity? Logo { get; set; }
    
    public  string Inn { get; set; } = null!;
    public bool IsPublic { get; set; }
    public virtual User Creator { get; set; } = null!;
    public virtual List<User> Users { get; set; } = new();
    public double MinPrice { get; set; }
    public virtual IEnumerable<ShopCategory> ShopCategory { get; set; } = null!;
    public virtual IEnumerable<ShopDelivery> ShopDeliveries { get; set; } = null!;
    public virtual IEnumerable<ShopPayment> ShopPayment { get; set; }= null!;
    public virtual IEnumerable<ShopTypes> ShopTypes { get; set; } = null!;
}