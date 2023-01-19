using data.Repository.Interface;
using Microsoft.Extensions.Configuration;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Http;

namespace logic.Service;

public class FileInfoService:IFileInfoService
{
    private readonly IConfiguration appConfig;
    private readonly IFileInfoRepository _fileInfoRepository;

    public FileInfoService(IConfiguration _appConfig, IFileInfoRepository fileInfoRepository)
    {
        this.appConfig = _appConfig;
        this._fileInfoRepository = fileInfoRepository;
    }
    public data.model.FileInfo Addfile(IFormFile file, int entityId)
    {
        var extension = Path.GetExtension(file.FileName);
        if (extension != ".png" && extension != ".jpg") throw new LogoException();
        data.model.FileInfo fi = new data.model.FileInfo()
        {
            Name = file.FileName,
            Extension = extension
        };
        _fileInfoRepository.Create(fi);
        _fileInfoRepository.Save();
        string fullPath = $"{appConfig["BasePath"]}/{entityId}/{fi.Id}{extension}";
        new DirectoryInfo($"{appConfig["BasePath"]}/{entityId}").Create();
        using (var fileStream = new FileStream(fullPath, FileMode.Append))
        {
            file.CopyToAsync(fileStream);
        }
        return fi;
    }
}