namespace Marketplace.DTO;

public class RegisterDto:UserDto
{
    public string Password { get; set; } = null!;
}