using System;
using System.Collections.Generic;
using System.Text;
using Identity.Core;
using Microsoft.Extensions.Configuration;


namespace Identity.Model.Dto
{
  public class EmailDto
  {
    
    public string FromAddress { get; set; }
    public string ToAddress { get; set; }
    //public string Username => _appSettings.SmtpUserName;
    //public string Password => _appSettings.SmtpPassword;
    //public string SmtpProvider => _appSettings.SmtpProvider;
    //public string SmtpPort => _appSettings.SmtpPort;

  }
}
