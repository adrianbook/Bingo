using DataTransfer.Communication;

namespace BingoHallTests;
[TestClass]
public class EmailingTests
{
    private static string html = @"
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <h1 style='color: blue;'>My Red Header</h1>
    <table style='border: 1px solid black; border-collapse: collapse;'>
        <tr>
            <th style='border: 1px solid black; padding: 15px;'>Header 1</th>
            <th style='border: 1px solid black; padding: 15px;'>Header 2</th>
        </tr>
        <tr>
            <td style='border: 1px solid black; padding: 15px;'>Data 1</td>
            <td style='border: 1px solid black; padding: 15px;'>Data 2</td>
        </tr>
        <tr>
            <td style='border: 1px solid black; padding: 15px;'>Data 3</td>
            <td style='border: 1px solid black; padding: 15px;'>Data 4</td>
        </tr>
    </table>
</body>
</html>";


    [TestMethod]
    public void TestEmailing()
    {
        var email = new EmailMessage(
                       new EmailMessageConfig(
                                    smtpServer: "dummy",
                                    smtpPort: 123,
                                  smtpUsername: "dummy",
                                  smtpPassword: "dummy"
                                  ));
        var message = email
            .AddRecipient("Adrian", "adrianbook@gmail.com")
            .SetSubject("Test")
            .SetPlainTextBody("plaintext as well!")
            .SetHtmlBody(html)
            .GetCompletedMimeMessageTestOnly();

        Assert.IsNotNull(message);
        Assert.AreEqual(message.Subject, "Test");
    }

    [TestMethod]
    public void TestMultipleRecipients()
    {
        var email = new EmailMessage(
                       new EmailMessageConfig(
                                    smtpServer: "dummy",
                                    smtpPort: 123,
                                  smtpUsername: "dummy",
                                  smtpPassword: "dummy"
                                  ));
        var message = email
            .AddRecipient("Adrian", "adrianbook@gmail.com")
            .AddRecipient("Adrian2", "adrianbook2@gmail.com")
            .SetSubject("Test")
            .SetPlainTextBody("plaintext as well!")
            .SetHtmlBody(html)
            .GetCompletedMimeMessageTestOnly();

        Assert.AreEqual(2, message.To.Count);
    }

    [TestMethod]
    public void AssertThrowsWithoutHtmlMessage()
    {
        var email = new EmailMessage(
               new EmailMessageConfig(
                            smtpServer: "dummy",
                            smtpPort: 123,
                          smtpUsername: "dummy",
                          smtpPassword: "dummy"
                          ));
        email
             .AddRecipient("Adrian", "adrianbook@gmail.com")
             .SetPlainTextBody("plaintext as well!")
             .SetSubject("Test");

        Assert.ThrowsException<InvalidOperationException>(() => email.GetCompletedMimeMessageTestOnly());
    }

    [TestMethod]
    public void AssertThrowsWithoutPlainTextMessage()
    {
        var email = new EmailMessage(
               new EmailMessageConfig(
                            smtpServer: "dummy",
                            smtpPort: 123,
                          smtpUsername: "dummy",
                          smtpPassword: "dummy"
                          ));
        email
             .AddRecipient("Adrian", "adrianbook@gmail.com")
             .SetHtmlBody(html)
             .SetSubject("Test");

        Assert.ThrowsException<InvalidOperationException>(() => email.GetCompletedMimeMessageTestOnly());
    }

    [TestMethod]
    public void AssertThrowsWithoutNoMessage()
    {
        var email = new EmailMessage(
               new EmailMessageConfig(
                            smtpServer: "dummy",
                            smtpPort: 123,
                          smtpUsername: "dummy",
                          smtpPassword: "dummy"
                          ));
        email
             .AddRecipient("Adrian", "adrianbook@gmail.com")
             .SetSubject("Test");

        Assert.ThrowsException<InvalidOperationException>(() => email.GetCompletedMimeMessageTestOnly());
    }

    [TestMethod]
    public void AssertThrowsWithoutSubject()
    {
        var email = new EmailMessage(
               new EmailMessageConfig(
                            smtpServer: "dummy",
                            smtpPort: 123,
                          smtpUsername: "dummy",
                          smtpPassword: "dummy"
                          ));
        email
             .AddRecipient("Adrian", "adrianbook@gmail.com")
             .SetHtmlBody(html)
             .SetPlainTextBody("plaintext as well!");

        Assert.ThrowsException<InvalidOperationException>(() => email.GetCompletedMimeMessageTestOnly());
    }


    [TestMethod]
    public void AssertThrowsWithoutRecipient()
    {
        var email = new EmailMessage(
               new EmailMessageConfig(
                            smtpServer: "dummy",
                            smtpPort: 123,
                          smtpUsername: "dummy",
                          smtpPassword: "dummy"
                          ));
        email
            .SetHtmlBody(html)
            .SetPlainTextBody("plaintext as well!")
            .SetSubject("Test");

        Assert.ThrowsException<InvalidOperationException>(() => email.GetCompletedMimeMessageTestOnly());
    }
}
