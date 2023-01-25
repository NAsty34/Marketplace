using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class DeliveryTypeRepository:BaseRepository<DeliveryType>, IDeliveryTypeRepository
{
    public DeliveryTypeRepository(DBContext _dbContext) : base(_dbContext, _dbContext.DeliveryTypes)
    {
    }
}