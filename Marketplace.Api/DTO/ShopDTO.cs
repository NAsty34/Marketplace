using System.Text.Json.Serialization;
using data.model;
using Microsoft.Extensions.Options;

namespace Marketplace.DTO;

public class ShopDto
{
    /*public static FileInfoOptions? Options;
    string _pathOptions = $"{Options.BaseUrl}/{Options.RequestPath}";*/
    
    public ShopDto()
    {
    }

    public ShopDto(ShopEntity shopEntity)
    {
        Id = shopEntity.Id;
        Name = shopEntity.Name;
        Description = shopEntity.Description;
        /*if (shopEntity.Logo != null)
        {
            Logo = $"{_pathOptions}/{shopEntity.Creator.Id}/{shopEntity.Logo.Id}{shopEntity.Logo.Extension}";
        }*/
        
        Inn = shopEntity.Inn;
        IsPublic = shopEntity.IsPublic;
        IsBlock = !shopEntity.IsActive;
        Owner = new UserDto(shopEntity.Creator);
        Categories = shopEntity.ShopCategory.Select(a => a.CategoryId);
        
        Types = shopEntity.ShopTypes.Select(a => a.TypeId);

        Payment = shopEntity.ShopPayment.Select(a => a.PaymentId);
        Com = shopEntity.ShopPayment.Select(a => a.Commission);
        

        Payments = shopEntity.ShopPayment.Select(a => new ShopPaymentDto()
        {
            IdPayment = a.PaymentId,
            Commision =a.Commission
        });
        Deliveris = shopEntity.ShopDeliveries.Select(a => new ShopDeliveryDto()
        {
            IdDelivery = a.DeliveryId,
            Price = a.Price
        });
        
        //MinPrice = _shop.ShopDeliveries.Select(a=>a.Price);
    }
    
    public  Guid Id { get; set; }
    [JsonIgnore]
    public  IEnumerable<Guid> Payment { get; set; } = null!;

    [JsonIgnore]
    public IEnumerable<double> Com { get; set; } = null!;

    public  IEnumerable<ShopPaymentDto> Payments { get; set; } = null!;
    public  string? Name { get; set; }
    public  string? Description { get; set; }
    public  string? Logo { get; set; }
    public  string? Inn { get; set; }
    public  IEnumerable<Guid> Categories { get; set; } = null!;

    [JsonIgnore]
    public  IEnumerable<Guid> Deliveri { get; set; } = null!;

    [JsonIgnore]
    public IEnumerable<double> MinPrice { get; set; } = null!;

    public IEnumerable<ShopDeliveryDto> Deliveris { get; set; } = null!;
    public  IEnumerable<Guid> Types { get; set; } = null!;
    public bool IsPublic { get; set; }
    public bool IsBlock { get; set; }
    public UserDto Owner { get; set; } = null!;
}