using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;

namespace logic.Service;

public class DeliveryTypeService:BaseService<DeliveryType>, IDeliveryTypeService
{
    private readonly IBaseRopository<DeliveryType> _repository;

    public DeliveryTypeService(IDeliveryTypeRepository _base) : base(_base)
    {
        _repository = _base;
    }

    public override DeliveryType Edit(DeliveryType t)
    {
        var FromDB = _repository.GetById(t.Id);
        FromDB.Name = t.Name;
        _repository.Save();
        return FromDB;
    }
}