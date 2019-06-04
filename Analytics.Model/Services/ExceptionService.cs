using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Model.Services
{
	public class ExceptionService : IExceptionService
	{
		public void Throw(Action action)
		{
			action();
		}
	}
}
