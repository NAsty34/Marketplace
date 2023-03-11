using data.model;
using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IFileInfoService
{
    Task<FileInfoEntity?> Addfile(IFormFile? file, Guid forPhoto, Guid userId);
    string GetUrlShop(ShopEntity shopEntity);
    List<string> GetUrlProduct(ProductEntity productEntity);
    /*public string GetUrl(FileInfoEntity? fi,  Guid? forId);*/
    public Task<IEnumerable<FileInfoEntity>> Addfiles(List<IFormFile> files, Guid forPhoto, Guid userId);
}