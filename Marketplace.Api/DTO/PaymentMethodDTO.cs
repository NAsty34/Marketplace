using data.model;

namespace Marketplace.DTO;

public class PaymentMethodDto:DictionaryDto<PaymentMethodEntity>
{
    public PaymentMethodDto()
    {
        
    }
    public PaymentMethodDto(PaymentMethodEntity t)
    {
        Id = t.Id;
        Name = t.Name;
        Commission = t.Commission;
    }
    public bool Commission { get; set; }
}