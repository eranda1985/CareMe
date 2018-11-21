using System;
namespace Vehicle.Model.Services
{
    public class ExceptionService: IExceptionService
    {
        public void Throw(Action action)
        {
            action();
        }
    }
}
