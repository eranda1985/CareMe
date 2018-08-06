using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model.Factory
{
  public interface IClient
  {
    void SendRequest(params object[] args);
  }
}
