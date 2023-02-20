namespace data.model;

public class ShopDictionaryBase
{
    public ShopDictionaryBase()
    {
    }

    public ShopDictionaryBase(Guid shopId, Guid guid)
    {
        ShopId = shopId;
    }
  //  public Guid Id { get; set; }
    public Guid ShopId { get; set; }
}