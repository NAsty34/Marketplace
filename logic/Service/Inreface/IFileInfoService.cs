using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IFileInfoService
{
    Task<data.model.FileInfo> Addfile(IFormFile file, Guid entityId);
}