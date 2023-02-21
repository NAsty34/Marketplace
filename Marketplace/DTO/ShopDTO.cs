using System.Text.Json.Serialization;
using data.model;

namespace Marketplace.DTO;

public class ShopDto
{
    private IConfiguration _configuration;
    
    public ShopDto()
    {
        
    }

    public ShopDto(Shop shop, IConfiguration configuration)
    {
        
        _configuration = configuration;
        var fileInfoOptions = new FileInfoOptions();
        configuration.GetSection(FileInfoOptions.File).Bind(fileInfoOptions);
        
        Id = shop.Id;
        Name = shop.Name;
        Description = shop.Description;
        if (shop.Logo != null)
        {
            Logo = $"{fileInfoOptions.BaseUrl}/{fileInfoOptions.RequestPath}/{shop.Creator.Id}/{shop.Logo.Id}{shop.Logo.Extension}";
            
        }
        
        Inn = shop.Inn;
        IsPublic = shop.IsPublic;
        IsBlock = shop.IsActive;
        Owner = new UserDto(shop.Creator);
        Categories = shop.ShopCategory.Select(a => a.CategoryId);
        
        Types = shop.ShopTypes.Select(a => a.TypeId);

        Payment = shop.ShopPayment.Select(a => a.PaymentId);
        Com = shop.ShopPayment.Select(a => a.Commission);
        

        Payments = shop.ShopPayment.Select(a => new ShopPaymentDto()
        {
            Commision =a.Commission,
            IdPayment = a.PaymentId
        });
        Deliveris = shop.ShopDeliveries.Select(a => new ShopDeliveryDto()
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