using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;

namespace logic.Service;

public class PaymentMethodService:BaseService<PaymentMethod>, IPaymentMethodService
{
    private readonly IBaseRopository<PaymentMethod> _repository;
    public PaymentMethodService(IBaseRopository<PaymentMethod> repository) : base(repository)
    {
        this._repository = repository;
    }
    public PaymentMethodService(IPaymentMethodRepository _base) : base(_base)
    {
        _repository = _base;
    }

    public override PaymentMethod Edit(PaymentMethod t)
    {
        var FromDB = _repository.GetById(t.Id);
        FromDB.Name = t.Name;
        _repository.Save();
        return FromDB;
    }
}