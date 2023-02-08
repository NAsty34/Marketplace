using System.Collections;
using System.Text.Json.Serialization;
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
        
        Types = _shop.ShopTypes.Select(a => a.TypeId);

        Payment = _shop.ShopPayment.Select(a => a.Paymentid);
        Com = _shop.ShopPayment.Select(a => a.commision);
        

        Payments = _shop.ShopPayment.Select(a => new ShopPaymentDTO()
        {
            commision =a.commision,
            IdPayment = a.Paymentid
        });
        Deliveris = _shop.ShopDeliveries.Select(a => new ShopDeliveryDTO()
        {
            IdDelivery = a.DeliveryId,
            Price = a.Price
        });
        
        //MinPrice = _shop.ShopDeliveries.Select(a=>a.Price);
    }
    
    public  Guid Id { get; set; }
    [JsonIgnore]
    public  IEnumerable<Guid> Payment { get; set; }
    [JsonIgnore]
    public IEnumerable<double> Com { get; set; }
    public  IEnumerable<ShopPaymentDTO> Payments { get; set; }
    public  string? Name { get; set; }
    public  string? Description { get; set; }
    public  string? Logo { get; set; }
    public  string? Inn { get; set; }
    public  IEnumerable<Guid> Categories { get; set; }
    [JsonIgnore]
    public  IEnumerable<Guid> Deliveri { get; set; }
    [JsonIgnore]
    public IEnumerable<double> MinPrice { get; set; }
    public IEnumerable<ShopDeliveryDTO> Deliveris { get; set; }
    public  IEnumerable<Guid> Types { get; set; }
    public bool isPublic { get; set; }
    public bool isBlock { get; set; }
    public UserDto Owner { get; set; }
    
}