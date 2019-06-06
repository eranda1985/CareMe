using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Model.Services
{
	public interface IExceptionService
	{
		void Throw(Action action);
	}
}
