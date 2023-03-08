using System.Net;
using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace logic.Service;

public class FileInfoService : IFileInfoService
{
    private FileInfoOptions _options;

    private readonly IFileInfoRepository _fileInfoRepository;
    //private ILogger<FileInfo> _logger;

    public FileInfoService(IFileInfoRepository fileInfoRepository, IConfiguration appConfig, ILogger<FileInfo> logger,
        IOptions<FileInfoOptions> options)
    {
        _fileInfoRepository = fileInfoRepository;
        _options = options.Value;
    }

    public string GetUrlShop(ShopEntity shopEntity)
    {
        var url =
            @$"C:\{_options.BasePath}\{shopEntity.Name}\{shopEntity.Creator.Name}\{shopEntity.Logo.Name}";
        return url;
    }

    public List<string> GetUrlProduct(ProductEntity productEntity)
    {
        var url =
            @$"C:\{_options.BasePath}\{productEntity.Name}\{productEntity.Creator.Name}\{productEntity.Photo.Name}";
        return new List<string> { url };
    }

    public async Task<FileInfoEntity?> Addfile(IFormFile? file, Guid forPhoto, Guid userId)
    {
        var extension = Path.GetExtension(file.FileName);

        if (extension is not ".png" and not ".jpg") throw new LogoException();
        FileInfoEntity fi = new FileInfoEntity()
        {
            Name = file.FileName,
            Extension = extension,
            EntityId = forPhoto
        };
        await _fileInfoRepository.Create(fi);
        await _fileInfoRepository.Save();

        string path = $@"C:\{_options.BasePath}";
        string subpath = $@"{forPhoto}\{userId}";
        DirectoryInfo dirInfo = new DirectoryInfo(path);

        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        dirInfo.CreateSubdirectory(subpath);

        string fullPath = $"C:\\{_options.BasePath}\\{forPhoto}\\{userId}\\{fi.Name}";

        using (var fileStream = new FileStream(fullPath, FileMode.Append))
        {
            await file.CopyToAsync(fileStream);
        }

        return fi;
    }
}