using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Services;
using System;
using System.Net.Mail;

namespace Identity.Model.Factory
{
	internal class SmtpServiceClient : IClient
	{
		private readonly IExceptionService _exceptionService;

		public string Username { get; set; }
		public string Password { get; set; }
		public string SmtpDomain { get; set; }
		public int SmtpPort { get; set; }

		public SmtpServiceClient(IExceptionService exceptionService)
		{
			_exceptionService = exceptionService;
		}

		/// <summary>
		/// Sends an e-mail. 
		/// </summary>
		/// <param name="args"></param>
		public void SendRequest(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 2));
			_exceptionService.Throw(() => Validator.CheckType<EmailDto>(args[0]));
			_exceptionService.Throw(() => Validator.CheckType<string>(args[1]));

			var emailObj = args[0] as EmailDto;
			var signUpCode = args[1] as string;

			SmtpClient smtpClient = new SmtpClient(SmtpDomain, SmtpPort)
			{
				Credentials = new System.Net.NetworkCredential(Username, Password),
				DeliveryMethod = SmtpDeliveryMethod.Network,
				EnableSsl = true
			};

			MailMessage mail = new MailMessage(emailObj.FromAddress, emailObj.ToAddress)
			{
				Subject = "New Sign-up Request",
				Body = string.Format("Please use the following code to sign-up: {0}", signUpCode)
			};

			smtpClient.Send(mail);
		}
	}
}
