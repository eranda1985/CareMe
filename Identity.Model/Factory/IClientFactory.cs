using Identity.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model.Factory
{
  public interface IClientFactory
  {
    IClient CreateFactory(ClientTypes type,IExceptionService exceptionService);
  }
}
