using data.model;
namespace data.Repository.Interface;

public interface IFileInfoRepository:IBaseRopository<FileInfoEntity>
{
    public Task DeleteRange(ProductEntity fromDb);
}

