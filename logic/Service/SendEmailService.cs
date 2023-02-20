using data.model;
using logic.Service.Inreface;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace logic.Service;

public class SendEmailService:ISendEmailService
{
    private readonly IConfiguration _appConfig;
    private ILogger<User> _logger;

    public SendEmailService(IConfiguration appConfig, ILogger<User> logger)
    {
        _appConfig = appConfig;
        _logger = logger;
    }
    public async Task Send(string email, string text, string header)
    {
        var adminoptions = new AdminOptions();
        _appConfig.GetSection(AdminOptions.Admin).Bind(adminoptions);
        //_logger.Log(LogLevel.Information, "===========Send========" + email);
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(adminoptions.NameSender, adminoptions.AdminEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = header;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = text
        };
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.mail.ru", 465, true);
            client.Authenticate(adminoptions.AdminEmail, adminoptions.EmailToken);
            client.Send(emailMessage);
            client.Disconnect(true);
        }
    }

    
}