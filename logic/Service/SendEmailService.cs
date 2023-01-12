using Dadata.Model;
using data.model;
using logic.Service.Inreface;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace logic.Service;

public class SendEmailService:ISendEmailService
{
    private readonly IConfiguration appConfig;

    public SendEmailService(IConfiguration _appConfig)
    {
        this.appConfig = _appConfig;
    }
    public void Send(string email, string text, string header)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(appConfig["NameSender"], appConfig["AdminEmail"]));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = header;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = text
        };
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.mail.ru", 465, true);
            client.Authenticate(appConfig["AdminEmail"], appConfig["Email-token"]);
            client.Send(emailMessage);
            client.Disconnect(true);
        }
    }

    
}