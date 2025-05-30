using Presentation.Models;

namespace Presentation.Services
{
    public interface ICommunicationService
    {
        Task<EmailResult> SendEmailAsync(string toEmail, string userName, string eventName);
    }
}