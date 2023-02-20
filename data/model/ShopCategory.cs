namespace data.model;

public class ShopCategory:ShopDictionaryBase
{
    public ShopCategory()
    {
        
    }
    public ShopCategory(Guid shopId, Guid guid) : base(shopId, guid)
    {
        ShopId = shopId;
        CategoryId = guid;
    }

    
    public Guid CategoryId { get; set; } 
}