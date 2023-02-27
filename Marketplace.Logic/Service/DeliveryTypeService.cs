using data.model;
using data.Repository.Interface;
using logic.Exceptions;

namespace logic.Service;

public class DeliveryTypeService:BaseService<DeliveryTypeEntity>
{
    public DeliveryTypeService(IBaseRopository<DeliveryTypeEntity> @base) : base(@base)
    {
        
    }

    public override async Task<DeliveryTypeEntity> Edit(DeliveryTypeEntity t)
    {
        var fromDb = await BaseRopository.GetById(t.Id);
        if (fromDb == null) throw new DeliveryNotFoundException();
        fromDb.Free = t.Free;
        fromDb.Name = t.Name;
        await BaseRopository.Save();
        return fromDb;
    }
}