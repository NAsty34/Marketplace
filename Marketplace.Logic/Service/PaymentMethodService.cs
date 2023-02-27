using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class PaymentMethodService:BaseService<PaymentMethodEntity>
{
    public PaymentMethodService(IBaseRopository<PaymentMethodEntity> @base) : base(@base)
    {
        
    }

    public override async Task<PaymentMethodEntity> Edit(PaymentMethodEntity t)
    {
        var fromDb = await BaseRopository.GetById(t.Id);
        if (fromDb == null)
        {
            throw new PyementNotFoundException();
        }

        fromDb.Commission = t.Commission;
        fromDb.Name = t.Name;
        await BaseRopository.Save();
        return fromDb;
    }
}