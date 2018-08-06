using Identity.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model.Factory
{
  public class ClientFactory : IClientFactory
  {
    public IClient CreateFactory(ClientTypes type, IExceptionService exceptionService)
    {
      switch (type)
      {
        case ClientTypes.SMTP:
          return new SmtpServiceClient(exceptionService);

        default:
          return null;
      }
    }
  }
}
