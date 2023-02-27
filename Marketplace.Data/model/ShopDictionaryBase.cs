namespace data.model;

public class ShopDictionaryBase
{
    public ShopDictionaryBase()
    {
    }

    public ShopDictionaryBase(Guid shopId, Guid guid)
    {
        ShopEntityId = shopId;
    }
  //  public Guid Id { get; set; }
    public Guid ShopEntityId { get; set; }
}