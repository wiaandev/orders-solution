namespace assessment.Application.Features.Mailing;

public interface IMailService
{
    Task<bool> SendMail(string recipientEmail);
}