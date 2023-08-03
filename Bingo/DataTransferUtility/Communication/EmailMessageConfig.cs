using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataTransferUtilityTest")]
namespace DataTransfer.Communication;

internal record EmailMessageConfig
{
    public EmailMessageConfig(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        SmtpServer=smtpServer;
        SmtpPort=smtpPort;
        SmtpUsername=smtpUsername;
        SmtpPassword=smtpPassword;
    }

    public string SmtpServer { get; init; }
    public int SmtpPort { get; init; }
    public string SmtpUsername { get; init; }
    public string SmtpPassword { get; init; }

}
