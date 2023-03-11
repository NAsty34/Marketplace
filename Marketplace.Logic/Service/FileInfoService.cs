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
    private static readonly HashSet<string> ALLOWED_EXTENSIONS = new HashSet<string>()
    {
        ".png", ".jpg"
    };

    private FileInfoOptions _options;

    private readonly IFileInfoRepository _fileInfoRepository;
    //private ILogger<FileInfo> _logger;

    public FileInfoService(IFileInfoRepository fileInfoRepository, IConfiguration appConfig, ILogger<FileInfo> logger,
        IOptions<FileInfoOptions> options)
    {
        _fileInfoRepository = fileInfoRepository;
        _options = options.Value;
    }

    /*public string GetUrl(FileInfoEntity? fi, Guid? forId)
    {
        var url =
            @$"C:\{_options.BasePath}\{forId}\{fi.EntityId}\{fi.Name}";
        return url;
    }*/
    

    public string GetUrlShop(ShopEntity shopEntity)
    {
        var url =
            $"{_options.BaseUrl}/{_options.RequestPath}/{shopEntity.Creator.Id}/{shopEntity.Logo.Id}{shopEntity.Logo.Extension}";
        return url;
    }

    public List<string> GetUrlProduct(ProductEntity productEntity)
    {
        string url = null;
        foreach (var foto in productEntity.Photo)
        { 
            url = $"{_options.BaseUrl}/{_options.RequestPath}/{productEntity.Creator.Id}/{foto.Id}{foto.Extension}";
        }

        return new List<string> { url };
    }

    public async Task<FileInfoEntity?> Addfile(IFormFile? file, Guid forPhoto, Guid userId)
    {
        var extension = Path.GetExtension(file.FileName);

        if (!ALLOWED_EXTENSIONS.Contains(extension)) throw new LogoException();
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

        string fullPath = $"C:\\{_options.BasePath}\\{forPhoto}\\{userId}\\{fi.Id}{fi.Extension}";

        using (var fileStream = new FileStream(fullPath, FileMode.Append))
        {
            await file.CopyToAsync(fileStream);
        }

        return fi;
    }

    public async Task<IEnumerable<FileInfoEntity>> Addfiles(List<IFormFile> files, Guid forPhoto, Guid userId)
    {
        List<FileInfoEntity> entities = files.Select(f =>
        {
            var extension = Path.GetExtension(f.FileName);
            if (!ALLOWED_EXTENSIONS.Contains(extension)) throw new LogoException();
            return new FileInfoEntity()
            {
                Name = f.FileName,
                Extension = extension,
                EntityId = forPhoto
            };
        }).ToList();
        
        await _fileInfoRepository.Create(entities);
        await _fileInfoRepository.Save();

        string path = $@"C:\{_options.BasePath}";
        string subpath = $@"{forPhoto}\{userId}";
        DirectoryInfo dirInfo = new DirectoryInfo(path);

        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        dirInfo.CreateSubdirectory(subpath);

        for (int i = 0; i < files.Count(); i++)
        {
            var file = files[i];
            string fullPath = $"C:\\{_options.BasePath}\\{forPhoto}\\{userId}\\{entities[i].Id}{entities[i].Extension}";

            using (var fileStream = new FileStream(fullPath, FileMode.Append))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        foreach (var file in files)
        {
            
        }

        return entities;
    }
}