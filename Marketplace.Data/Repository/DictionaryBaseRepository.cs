using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class DictionaryBaseRepository:BaseRepository<DictionaryBaseEntity>, IDictionaryBaseRepository
{
    public DictionaryBaseRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }
    
}