using System;
namespace RunningData.Model.Services
{
    public class ExceptionService: IExceptionService
    {
        public void Throw(Action action)
        {
            action();
        }
    }
}
