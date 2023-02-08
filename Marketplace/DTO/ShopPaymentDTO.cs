using data.model;

namespace Marketplace.DTO;

public class ShopPaymentDTO
{

    public ShopPaymentDTO()
    {
        
    }

    /*public ShopPaymentDTO(ShopDTO t)
    {
        IdPayment = t.Payment;
        commision = t.Com;
    }*/
    public Guid IdPayment { get; set; }
    public double commision { get; set; }
}