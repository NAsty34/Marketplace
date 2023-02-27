using Microsoft.Extensions.Options;

namespace data.model;

public class FileInfoOptions
{
    public const string File = "File";
    public string BasePath { get; set; } = String.Empty;
    public string BaseUrl { get; set; } = String.Empty;
    public string RequestPath { get; set; } = String.Empty;
}