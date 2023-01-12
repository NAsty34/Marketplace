using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;

namespace logic.Service;

public class HashService:IHashService
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string text, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(text, hash);
    }
}