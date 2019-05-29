using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Api.Extensions
{
	public static class IsAExtension
	{
		public static bool IsA<T>(this object obj)
		{
			return (obj.GetType() == typeof(T));
		}
	}
}
