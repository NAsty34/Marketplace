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
            $"{_options.BaseUrl}/{_options.RequestPath}/{shopEntity.Creator.Id}/{shopEntity.Logo.Id}{shopEntity.Logo.Extension}";
        return url;
    }

    public string GetUrlProduct(ProductEntity productEntity)
    {
        var url =
            $"{_options.BaseUrl}/{_options.RequestPath}/{productEntity.Creator.Id}/{productEntity.Photo.Id}{productEntity.Photo.Extension}";
        return url;
    }

    public async Task<FileInfoEntity?> Addfile(IFormFile? file, Guid entityId)
    {
        var extension = Path.GetExtension(file.FileName);

        if (extension is not ".png" and not ".jpg") throw new LogoException();
        FileInfoEntity fi = new FileInfoEntity()
        {
            Name = file.FileName,
            Extension = extension
        };
        await _fileInfoRepository.Create(fi);
        await _fileInfoRepository.Save();
        string fullPath = $"{_options.BasePath}/{entityId}/{fi.Id}{extension}";
       
        using (var fileStream = new FileStream(fullPath, FileMode.Append))
        {
            await file.CopyToAsync(fileStream);
        }

        return fi;
    }
}