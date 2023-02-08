using data.model;

namespace Marketplace.DTO;

public class PaymentMethodDTO:DictionaryDTO<PaymentMethod>
{
    public PaymentMethodDTO()
    {
        
    }
    public PaymentMethodDTO(PaymentMethod t)
    {
        t.Id = Id;
        t.Name = Name;
        Commission = t.Commission;
    }
    public bool Commission { get; set; }
}