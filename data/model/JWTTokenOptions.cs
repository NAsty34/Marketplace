namespace data.model;

public class JWTTokenOptions
{
    public const string JWTToken = "JWTToken";
    public string ISSUER { get; set; } = String.Empty;
    public string AUDIENCE { get; set; } = String.Empty;
    public string KEY { get; set; } = String.Empty;
}