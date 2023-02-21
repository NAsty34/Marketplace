using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace logic.Service;

public class FileInfoService : IFileInfoService
{
    private readonly IConfiguration _appConfig;

    private readonly IFileInfoRepository _fileInfoRepository;
    // private ILogger<FileInfo> _logger;

    public FileInfoService(IFileInfoRepository fileInfoRepository, IConfiguration appConfig)
    {
        _appConfig = appConfig;
        //_logger = logger;
        _fileInfoRepository = fileInfoRepository;
    }

    public async Task<FileInfoEntity> Addfile(IFormFile file, Guid entityId)
    {
        var fileInfoOptions = new FileInfoOptions();
        _appConfig.GetSection(FileInfoOptions.File).Bind(fileInfoOptions);

        var extension = Path.GetExtension(file.FileName);
        if (extension is not ".png" and not ".jpg") throw new LogoException();
        FileInfoEntity fi = new FileInfoEntity()
        {
            Name = file.FileName,
            Extension = extension
        };
        await _fileInfoRepository.Create(fi);
        await _fileInfoRepository.Save();
        string fullPath = $"{fileInfoOptions.BasePath}/{entityId}/{fi.Id}{extension}";
        new DirectoryInfo($"{fileInfoOptions.BasePath}/{entityId}").Create();
        //_logger.Log(LogLevel.Information, "======" + $"{fileInfoOptions.BaseUrl}/{fileInfoOptions.RequestPath}");
        using (var fileStream = new FileStream(fullPath, FileMode.Append))
        {
            await file.CopyToAsync(fileStream);
        }

        return fi;
    }
}