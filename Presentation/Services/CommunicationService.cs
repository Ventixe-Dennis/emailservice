using System.Diagnostics;
using Azure;
using Azure.Communication.Email;
using Presentation.Models;


namespace Presentation.Services;

public class CommunicationService(EmailClient client, IConfiguration configuration) : ICommunicationService
{
    private readonly EmailClient _client = client;
    private readonly IConfiguration _configuration = configuration;

    public async Task<EmailResult> SendEmailAsync(string toEmail, string userName, string eventName)
    {
        try
        {
            var subject = $"Bokningsbekräftelse för {eventName}";
            var plainTextContent = $@"
                Hej {userName},

               Tack för din bokning av {eventName}!
                
               Hoppas du får en fantastisk upplevelse!

               Med Vänliga Hälsningar,
               Ventixe Events



                ";
            var htmlContent = $@"
                <html>
                    <body>
                        <h2> Hej {userName}</h2>
                        <p>Tack för din bokning av  <strong>{eventName}</strong>.</p>
                        <p>Hoppas du får en fantastisk upplevelse!</p>
                        <br/>
                        <p>Med Vänliga Hälsningars,<br/>Ventixe Events</p>
                    </body>
                </html>";

            var emailMessage = new EmailMessage(
                senderAddress: _configuration["Azure:SenderAdress"],
                recipients: new EmailRecipients([new(toEmail)]),
                content: new EmailContent(subject)
                {
                    Html = htmlContent,
                    PlainText = plainTextContent,
                }

            );

            var emailSendOperation = await _client.SendAsync(WaitUntil.Started, emailMessage);
            return new EmailResult { Success = true };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new EmailResult { Success = false, Error = "Failed to send Confirmation Mail" };
        }
    }
}

