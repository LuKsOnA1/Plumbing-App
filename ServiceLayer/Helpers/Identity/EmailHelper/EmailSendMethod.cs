using EntityLayer.Identity.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Helpers.Identity.EmailHelper
{
	public interface IEmailSendMethod
	{
		Task SendPasswordResetLinkWithToken(string passwordResetLink, string toEmail);
	}
	public class EmailSendMethod : IEmailSendMethod
	{
		private readonly GmailInformationsVM _emailInfo;

        public EmailSendMethod(IOptions<GmailInformationsVM> emailInfo)
        {
			_emailInfo = emailInfo.Value;
        }
        public async Task SendPasswordResetLinkWithToken(string passwordResetLink, string toEmail)
		{
			// Here is Smtp client configuration ...
			var smtpClient = new SmtpClient();

			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Host = _emailInfo.Host;
			smtpClient.Port = 587;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(_emailInfo.Email, _emailInfo.Password);
			smtpClient.EnableSsl = true;

            // Here is Mail Message configuration ...
            var mailMessage = new MailMessage();

			mailMessage.From = new MailAddress(_emailInfo.Email);
			mailMessage.To.Add(toEmail);
			mailMessage.Subject = "Password Reset Link | Plumbing";
			mailMessage.Body = $@"<h1> PASSWORD RESET LINK </h1>
								  <h5> Click <a href='{passwordResetLink}'>Here</a> to reset your password </h5>";
			mailMessage.IsBodyHtml = true;

			// Send ...
			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
