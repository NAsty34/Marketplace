using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FileInfoRepository:BaseRepository<FileInfoEntity>, IFileInfoRepository
{
    public FileInfoRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }

    public async Task DeleteRange(ProductEntity fromDb)
    {
        var all = await DbSet.Where(a => a.EntityId == fromDb.Id).ToListAsync();
        DbSet.RemoveRange(all);
        if (fromDb.PhotoId != null)
        {
            fromDb.PhotoId.Clear();
        }
        
    }
}