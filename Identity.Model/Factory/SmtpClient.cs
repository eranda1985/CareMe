using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Services;
using System.Net.Mail;

namespace Identity.Model.Factory
{
  class SmtpServiceClient : IClient
  {
    private readonly IExceptionService _exceptionService;

    public string Username { get; set; }
    public string Password { get; set; }
    public string SmtpDomain  { get; set; }
    public int SmtpPort { get; set; }

    public SmtpServiceClient(IExceptionService exceptionService)
    {
      _exceptionService = exceptionService;
    }
    public void SendRequest(params object[] args)
    {
      _exceptionService.Throw(() => Validator.CheckArgsLength(args, 1));
      _exceptionService.Throw(() => Validator.CheckType<EmailDto>(args[0]));

      var emailObj = args[0] as EmailDto;

      SmtpClient smtpClient = new SmtpClient(SmtpDomain, SmtpPort);
      smtpClient.Credentials = new System.Net.NetworkCredential(Username, Password);
      //smtpClient.UseDefaultCredentials = false;
      smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtpClient.EnableSsl = true;
      MailMessage mail = new MailMessage(emailObj.FromAddress, emailObj.ToAddress);
      mail.Subject = "Test email";
      mail.Body = "Please use the following code to sign-up: 0000";

      smtpClient.Send(mail);

    }
  }
}
