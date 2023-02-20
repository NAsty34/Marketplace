using data.model;

namespace Marketplace.DTO;

public class DeliveryTypeDto:DictionaryDto<DeliveryType>
{
    public DeliveryTypeDto()
    {
        
    }
    public DeliveryTypeDto(DeliveryType t)
    {
        t.Id = Id;
        t.Name = Name;
        t.Free = Free;
    }
    public bool Free { get; set; }
}