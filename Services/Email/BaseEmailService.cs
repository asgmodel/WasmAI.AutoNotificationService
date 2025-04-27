using Microsoft.AspNetCore.Http;


using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using AutoNotificationService.Services.Base;
namespace AutoNotificationService.Services.Email;

public interface IMailConfiguration
{
    string SmtpHost { get; }
    int SmtpPort { get; }
    string SmtpUsername { get; }
    string SmtpPassword { get; }
    string FromEmail { get; }
    public string NameApp { get; set; }
}

public class MailConfiguration : IMailConfiguration
{
    public required string SmtpHost { get; set; }
    public required int SmtpPort { get; set; }
    public required string SmtpUsername { get; set; }
    public required  string SmtpPassword { get; set; }
    public required string FromEmail { get; set; }

    public required string  NameApp {  get; set; }
}
public class BaseEmailService : BaseNotificationService<ITEmailModel, MimeMessage, EmailModel, BatchEmailModel>,IBaseEmailService
{
    private readonly IMailConfiguration _config;

    public BaseEmailService(IMailConfiguration config)
    {
        _config = config;
    }




    private MimeMessage CreateOneMessage(string? to, string? subject, string? body, IList<IFormFile>? attachments = null)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_config.NameApp ?? "App", _config.FromEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };

        if (attachments != null)
        {
            foreach (var file in attachments)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                builder.Attachments.Add(file.FileName, ms.ToArray());
            }
        }

        message.Body = builder.ToMessageBody();
        return message;
    }

    private MimeMessage CreateListMessage(List<string>? to, string? subject, string? body, IList<IFormFile>? attachments = null)
    {
       
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_config.NameApp ?? "App", _config.FromEmail));
        message.To.AddRange(to.Select(MailboxAddress.Parse));
        message.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };

        if (attachments != null)
        {
            foreach (var file in attachments)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                builder.Attachments.Add(file.FileName, ms.ToArray());
            }
        }

        message.Body = builder.ToMessageBody();
        return message;
    }

    private async Task<ResultCommon> sendAsync(MimeMessage message)
    {
        try
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config.SmtpHost, _config.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            return ResultCommon.Success();
        }
        catch (Exception ex)
        {
            return ResultCommon.Failure($"MailKit error: {ex.Message}");
        }
    }

    protected override Task<ResultCommon> SendTo(MimeMessage data)
    {
        return sendAsync(data);
    }

    protected override MimeMessage MapTo<T>(T data)
    {
        if (data is BatchEmailModel batch)
            return CreateListMessage(batch.ToEmails, batch.Subject, batch.Body, batch.Attachments);
        else if (data is EmailModel email)
            return CreateOneMessage(email.ToEmail, email.Subject, email.Body, email.Attachments);


            throw new NotImplementedException();
    }
}