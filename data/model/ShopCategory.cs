namespace data.model;

public class ShopCategory:ShopDictionaryBase
{
    public ShopCategory()
    {
        
    }
    public ShopCategory(Guid shopid, Guid guid) : base(shopid, guid)
    {
        this.shopid = shopid;
        this.CategoryId = guid;
    }

  
    public Guid CategoryId { get; set; } 
}