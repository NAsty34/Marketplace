
namespace logic.Service.Inreface;

public interface ISendEmailService
{
    Task Send(string email, string text, string header);
    
}