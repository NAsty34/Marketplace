namespace data.model;

public class JwtTokenOptions
{
    public const string JwtToken = "JWTToken";
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public string Key { get; set; } = String.Empty;
}