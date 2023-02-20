using data.model;

namespace Marketplace.DTO;

public class PaymentMethodDto:DictionaryDto<PaymentMethod>
{
    public PaymentMethodDto()
    {
        
    }
    public PaymentMethodDto(PaymentMethod t)
    {
        t.Id = Id;
        t.Name = Name;
        Commission = t.Commission;
    }
    public bool Commission { get; set; }
}