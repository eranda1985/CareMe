using System;
namespace Identity.Model.Services
{
    public interface IExceptionService
    {
        void Throw(Action action);
    }
}
