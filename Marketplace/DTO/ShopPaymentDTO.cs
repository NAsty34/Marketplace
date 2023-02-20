
namespace Marketplace.DTO;

public class ShopPaymentDto
{

    public ShopPaymentDto()
    {
        
    }

    /*public ShopPaymentDTO(ShopDTO t)
    {
        IdPayment = t.Payment;
        commision = t.Com;
    }*/
    public Guid IdPayment { get; set; }
    public double Commision { get; set; }
}