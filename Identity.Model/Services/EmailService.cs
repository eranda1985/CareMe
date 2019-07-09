using Identity.Core;
using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Factory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model.Services
{
  public class EmailService : IService<EmailDto>
  {
    private EmailDto _emailDto;
    private readonly IConfiguration _configuration;
    private readonly AppSettings _appSettings;
    private readonly IExceptionService _exceptionService;
    private IClientFactory _clientFactory;

    public EmailService(IConfiguration configuration, IExceptionService exceptionService, IClientFactory clientFactory)
    {
      _configuration = configuration;
      _appSettings = _configuration.Get<AppSettings>();
      _exceptionService = exceptionService;
      _clientFactory = clientFactory;
    }

    public void SendMail(string from, string to, string code)
    {
      _emailDto = new EmailDto { FromAddress = from, ToAddress = to };

      var smtpFac = _clientFactory.CreateFactory(ClientTypes.SMTP, _exceptionService) as SmtpServiceClient;

      _exceptionService.Throw(() => Validator.CheckNull(smtpFac));

      smtpFac.Username = _appSettings.SmtpUserName;
      smtpFac.Password = _appSettings.SmtpPassword;
      smtpFac.SmtpDomain = _appSettings.SmtpProvider;
      smtpFac.SmtpPort = _appSettings.SmtpPort;
      smtpFac.SendRequest(_emailDto, code); // <- Potential place to implement resilient connections. 
    }

		/// <summary>
		/// Generates a 4 digit code
		/// </summary>
		/// <returns></returns>
		public string Get4DigitCode()
		{
			Random r = new Random();
			int result = r.Next(0, 10000);
			return result.ToString("D4");
		}
	}
}
