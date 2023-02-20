using data.Repository.Interface;

namespace data.Repository;

public class FileInfoRepository:BaseRepository<data.model.FileInfo>, IFileInfoRepository
{
    public FileInfoRepository(DBContext dbContext) : base(dbContext)
    {
    }
}