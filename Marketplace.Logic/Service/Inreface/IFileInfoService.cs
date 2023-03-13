using data.model;
using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IFileInfoService
{
    Task<FileInfoEntity?> Addfile(IFormFile? file, Guid forPhoto, Guid userId);
    string GetUrlShop(ShopEntity shopEntity);
    List<string> GetUrlProduct(ProductEntity productEntity);
    public List<Guid> GetFilesId(IEnumerable<FileInfoEntity> files);
    public Task<IEnumerable<FileInfoEntity>> Addfiles(List<IFormFile> files, Guid forPhoto, Guid userId);
}