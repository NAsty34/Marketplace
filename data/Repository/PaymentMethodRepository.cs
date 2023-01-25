using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository.Interface;

public class PaymentMethodRepository:BaseRepository<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(DBContext _dbContext) : base(_dbContext, _dbContext.PaymentMethods)
    {
    }
}