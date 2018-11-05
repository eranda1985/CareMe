using System;
namespace RunningData.Model.Services
{
    public interface IExceptionService
    {
        void Throw(Action action);
    }
}
