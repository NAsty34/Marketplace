using data.model;

namespace Marketplace.DTO;

public class DeliveryTypeDto:DictionaryDto<DeliveryTypeEntity>
{
    public DeliveryTypeDto()
    {
        
    }
    public DeliveryTypeDto(DeliveryTypeEntity t)
    {
        t.Id = Id;
        t.Name = Name;
        t.Free = Free;
    }
    public bool Free { get; set; }
}