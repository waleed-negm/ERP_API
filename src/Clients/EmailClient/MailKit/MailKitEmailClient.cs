using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Clients.EmailClient.VewModels;
using Clients.EmailClient.Interfaces;
using Clients.EmailClient.Configuration;

namespace Clients.EmailClient.MailKit
{
	public class MailKitEmailClient : IMailKitEmailClient
	{
		private readonly EmailConfiguration _emailConfiguration;
		private readonly IViewRender _viewRender;

		public MailKitEmailClient(IViewRender viewRender, IOptionsMonitor<EmailConfiguration> emailConfig)
		{
			_emailConfiguration = emailConfig.CurrentValue ?? throw new ArgumentNullException(nameof(EmailConfiguration));
			_viewRender = viewRender ?? throw new ArgumentNullException(nameof(viewRender));
		}

		public string CreateEmailBody(EmailViewModel emailModel)
		{
			var emailBody = File.ReadAllText("./Views/Emails/EmailTemplate.html");
			var body = _viewRender.Render(emailBody, emailModel);
			return body;
		}

		public async Task SendEmailAsync(string mailTo, string subject, string body)
		{
			var email = new MimeMessage
			{
				Sender = MailboxAddress.Parse(_emailConfiguration.smtpUsername),
				Subject = subject
			};
			email.To.Add(MailboxAddress.Parse(mailTo));
			var builder = new BodyBuilder
			{
				HtmlBody = body
			};
			email.Body = builder.ToMessageBody();
			email.From.Add(new MailboxAddress(_emailConfiguration.smtpUsername, _emailConfiguration.smtpUsername));
			using var smtp = new SmtpClient();
			smtp.Connect(_emailConfiguration.smtpServer, _emailConfiguration.smtpPort, SecureSocketOptions.StartTls);
			smtp.Authenticate(_emailConfiguration.smtpUsername, _emailConfiguration.smtpPassword);
			await smtp.SendAsync(email);
			smtp.Disconnect(true);
		}
	}
}
