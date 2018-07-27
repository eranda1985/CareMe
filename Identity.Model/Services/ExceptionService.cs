using System;
namespace Identity.Model.Services
{
    public class ExceptionService: IExceptionService
    {
        public ExceptionService()
        {
        }

        public void Throw(Action action)
        {
            action();
        }
    }
}
