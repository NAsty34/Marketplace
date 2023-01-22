using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IFileInfoService
{
    data.model.FileInfo Addfile(IFormFile file, Guid entityId);
}