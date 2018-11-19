using System;
namespace Vehicle.Model.Services
{
    public interface IExceptionService
    {
        void Throw(Action action);
    }
}
