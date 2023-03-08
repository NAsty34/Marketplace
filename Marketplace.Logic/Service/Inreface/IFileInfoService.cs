using data.model;
using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IFileInfoService
{
    Task<FileInfoEntity?> Addfile(IFormFile? file, Guid forPhoto, Guid userId);
    string GetUrlShop(ShopEntity shopEntity);
    List<string> GetUrlProduct(ProductEntity productEntity);
}