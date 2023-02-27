using data.model;
using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IFileInfoService
{
    Task<FileInfoEntity?> Addfile(IFormFile? file, Guid entityId);
    string GetUrlShop(ShopEntity shopEntity);
    string GetUrlProduct(ProductEntity productEntity);
}