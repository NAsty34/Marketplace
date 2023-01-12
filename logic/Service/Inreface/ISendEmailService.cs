using data.model;

namespace logic.Service.Inreface;

public interface ISendEmailService
{
    void Send(string email, string text, string header);
    
}