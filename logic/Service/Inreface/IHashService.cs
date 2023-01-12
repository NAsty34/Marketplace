namespace logic.Service.Inreface;

public interface IHashService
{
    string Hash(string password);
    bool Verify(string text, string hash);
}