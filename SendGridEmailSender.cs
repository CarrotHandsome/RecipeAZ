using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

public class SendGridEmailSender : IEmailSender {
    private readonly IConfiguration _configuration;

    public SendGridEmailSender(IConfiguration configuration) {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
        var apiKey = _configuration.GetSection("SendGrid")["ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("carrothands@gmail.com", "CarrotHands");
        var to = new EmailAddress(email);
        var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
        var response = await client.SendEmailAsync(message);
        if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted) {
            throw new SendEmailException($"Failed to send email to {email}. Status code: {response.StatusCode}");
        }
    }
}

public class SendEmailException : System.Exception {
    public SendEmailException(string message) : base(message) {
    }
}