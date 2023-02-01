using data;
using data.model;

namespace Marketplace.DTO;

public class ShopDTO
{

    public ShopDTO()
    {
    }

    public ShopDTO(Shop _shop, IConfiguration appConfig)
    {
        Id = _shop.Id;
        Name = _shop.Name;
        Description = _shop.Description;
        if (_shop.Logo != null)
        {
            Logo = $"{appConfig["BaseUrl"]}/{appConfig["RequestPath"]}/{_shop.Id}/{_shop.Logo.Id}{_shop.Logo.Extension}";
        }
        
        Inn = _shop.Inn;
        isPublic = _shop.isPublic;
        isBlock = _shop.IsActive;
        Owner = new UserDto(_shop.Creator);
        Categories = _shop.ShopCategory.Select(a => a.CategoryId);
        Deliveris = _shop.ShopDeliveries.Select(a => a.DeliveryId);
        Types = _shop.ShopTypes.Select(a => a.TypeId);
        Payments = _shop.ShopPayment.Select(a => a.Paymentid);
    }
    public  Guid Id { get; set; }
    
    public  IEnumerable<Guid> Categories { get; set; }
    public  IEnumerable<Guid> Deliveris { get; set; }
    public  IEnumerable<Guid> Types { get; set; }
    public  IEnumerable<Guid> Payments { get; set; }
    public  string? Name { get; set; }
    public  string? Description { get; set; }
    public  string? Logo { get; set; }
    public  string? Inn { get; set; }
    public bool isPublic { get; set; }
    public bool isBlock { get; set; }
    public UserDto Owner { get; set; }
    
}