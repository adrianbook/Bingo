using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BingoHallTests")]
namespace BingoHall.Communication;

internal class EmailMessage
{
    private readonly EmailMessageConfig _config;

    private readonly MimeMessage _message;

    private readonly BodyBuilder _bodyBuilder;
    internal EmailMessage(EmailMessageConfig emailMessageConfig)
    {
        _config=emailMessageConfig??throw new ArgumentNullException(nameof(emailMessageConfig));
        _message=new MimeMessage();
        _message.From.Add(new MailboxAddress("Bingo Application", _config.SmtpUsername));
        _bodyBuilder=new BodyBuilder();
    }

    internal MimeMessage GetCompletedMimeMessageTestOnly()
    {
        PackageAndValidateMessage();
        return _message;
    }

    public EmailMessage SetSubject(string subject)
    {
        _message.Subject=subject??throw new ArgumentNullException(nameof(subject));
        return this;
    }

    public EmailMessage SetPlainTextBody(string plaintext)
    {
        if (string.IsNullOrEmpty(plaintext)) { throw new ArgumentNullException(nameof(plaintext)); }
        _bodyBuilder.TextBody=plaintext;
        return this;
    }

    public EmailMessage SetHtmlBody(string html)
    {
        if (string.IsNullOrEmpty(html)) { throw new ArgumentNullException(nameof(html)); }
        _bodyBuilder.HtmlBody=html;
        return this;
    }

    public EmailMessage AddRecipient(string name, string adress)
    {
        if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
        if (string.IsNullOrWhiteSpace(adress)) { throw new ArgumentNullException(nameof(adress)); }
        if (!adress.Contains("@")) { throw new ArgumentException("Invalid email adress", nameof(adress)); }
        _message.To.Add(new MailboxAddress(name, adress));
        return this;
    }

    public void Send()
    {
        PackageAndValidateMessage();
        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_config.SmtpServer, _config.SmtpPort, false);
            smtp.Authenticate(_config.SmtpUsername, _config.SmtpPassword);
            smtp.Send(_message);
            smtp.Disconnect(true);
        }
    }

    public async Task SendAsync()
    {
        PackageAndValidateMessage();
        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_config.SmtpServer, _config.SmtpPort, false);
            smtp.Authenticate(_config.SmtpUsername, _config.SmtpPassword);
            await smtp.SendAsync(_message);
            smtp.Disconnect(true);
        }
    }

    private void PackageAndValidateMessage()
    {
        SetBody();
        if (_message.To.IsNullOrEmpty()) { throw new InvalidOperationException("Email must have a recipient"); }
        if (string.IsNullOrEmpty(_message.Subject)) { throw new InvalidOperationException("Email must have a subject"); }
        if (string.IsNullOrWhiteSpace(_message.HtmlBody) || string.IsNullOrWhiteSpace(_message.TextBody)) { throw new InvalidOperationException("Email must have a body"); }
    }

    private void SetBody()
    {
        _message.Body=_bodyBuilder.ToMessageBody();
    }
}
