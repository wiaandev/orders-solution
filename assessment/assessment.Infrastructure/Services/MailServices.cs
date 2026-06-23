using assessment.Application.Features.Mailing;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace assessment.Infrastructure.Services;

public class MailServices(IConfiguration configuration) : IMailService
{
    public Task<bool> SendMail(string recipientEmail)
    {
        try
        {
            var host = configuration["Smtp:Host"] ?? "smtp.gmail.com";
            var port = int.TryParse(configuration["Smtp:Port"], out var configuredPort)
                ? configuredPort
                : 587;
            var username = configuration["Smtp:Username"];
            var password = configuration["Smtp:Password"];
            var senderEmail = configuration["Smtp:SenderEmail"] ?? username;
            var enableSsl = !bool.TryParse(configuration["Smtp:EnableSsl"], out var configuredSsl) || configuredSsl;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(senderEmail))
            {
                return Task.FromResult(false);
            }

            using var smtp = new SmtpClient(host, port);
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.EnableSsl = enableSsl;
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.EnableSsl = enableSsl;

            using var mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.Subject = "Order Confirmation";
            mail.Body = "Get excited! Your order has been placed";

            mail.To.Add(recipientEmail);
            smtp.Send(mail);

            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}