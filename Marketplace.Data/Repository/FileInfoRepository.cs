using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class FileInfoRepository:BaseRepository<FileInfoEntity>, IFileInfoRepository
{
    public FileInfoRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }
}