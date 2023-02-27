namespace data.model;

public class JwtTokenOptions
{
    public const string JwtToken = "JWTToken";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int Time { get; set; } 
}