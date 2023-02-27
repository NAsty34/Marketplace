using data.model;
using logic.Service.Inreface;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace logic.Service;

public class SendEmailService:ISendEmailService
{
    private readonly AdminOptions _options;
    //private ILogger<User> _logger;

    public SendEmailService(IOptions<AdminOptions> options)
    {
        _options = options.Value;
        //_logger = logger;
    }
    public async Task Send(string email, string text, string header)
    {
        //_logger.Log(LogLevel.Information, "===========Send========" + email);
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_options.NameSender, _options.AdminEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = header;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = text
        };
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.mail.ru", 465, true);
            client.Authenticate(_options.AdminEmail, _options.EmailToken);
            client.Send(emailMessage);
            client.Disconnect(true);
        }
    }

    
}