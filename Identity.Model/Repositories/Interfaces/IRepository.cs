using System;
using System.Collections.Generic;

namespace Identity.Model.Repositories.Interfaces
{
    public interface IRepository<T> where T: class
    {
        List<T> GetAll();
    }
}
