using data.model;

namespace Marketplace.DTO;

public class DeliveryTypeDTO:DictionaryDTO<DeliveryType>
{
    public DeliveryTypeDTO()
    {
        
    }
    public DeliveryTypeDTO(DeliveryType t)
    {
        t.Id = Id;
        t.Name = Name;
        t.Free = Free;
    }
    public bool Free { get; set; }
}