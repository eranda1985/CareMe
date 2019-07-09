using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Services;
using System;
using System.Net.Mail;
using System.Net.Mime;

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
		/// <param name='args'></param>
		public void SendRequest(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 3));
			_exceptionService.Throw(() => Validator.CheckType<EmailDto>(args[0]));
			_exceptionService.Throw(() => Validator.CheckType<string>(args[1]));
			_exceptionService.Throw(() => Validator.CheckType<string>(args[2]));

			var emailObj = args[0] as EmailDto;
			var signUpCode = args[1] as string;
			var logoFilePath = args[2] as string;

			SmtpClient smtpClient = new SmtpClient(SmtpDomain, SmtpPort)
			{
				Credentials = new System.Net.NetworkCredential(Username, Password),
				DeliveryMethod = SmtpDeliveryMethod.Network,
				EnableSsl = true
			};

			var img = new LinkedResource(string.Format("{0}/logo-color.png", logoFilePath))
			{
				ContentId = Guid.NewGuid().ToString()
			};
			var mailBody = string.Format(HtmlBody, signUpCode, "\u00a9", img.ContentId);
			AlternateView alternateView = AlternateView.CreateAlternateViewFromString(mailBody, null, MediaTypeNames.Text.Html);
			alternateView.LinkedResources.Add(img);

			MailMessage mail = new MailMessage(emailObj.FromAddress, emailObj.ToAddress)
			{
				Subject = "New Sign-up Request"
			};
			mail.IsBodyHtml = true;
			mail.AlternateViews.Add(alternateView);

			smtpClient.Send(mail);
		}

		private string HtmlBody => @"<body style='margin:0px; padding:0px;' bgcolor='#efefef'>
<table align='center' border='0' cellspacing='0' cellpadding='0' ><tr><td valign='top' align='center'><img src='cid:{2}' alt='Smiley face' /><br /><br /><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'><tbody><tr><td style='font-family:'Open Sans', Arial, sans-serif; font-size:12px; line-height:15px; color:#0d1121;' valign='top' align='center'>
<h2>New sign up request for Cargenik App{1}</h2>
<table align = 'center' border='0' cellspacing='0' cellpadding='0'>
<tr><td align = 'center' valign='top' style='font-family:'Open Sans', Arial, sans-serif; font-size:16px; line-height:30px; color:#000000;'>
<h3>Please use  the following code to sign up: <b> {0} </b></h3>
</td></tr><tr><td height = '15' style='font-size:0px; line-height:0px; height:15px;'>&nbsp;</td></tr></table></td></tr></tbody></table></td></tr></table></body>";
	}
}
