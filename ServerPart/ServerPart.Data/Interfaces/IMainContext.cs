using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Interfaces
{
    public interface IMainContext<T>: IDisposable where T: class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(int itemId);
    }
}
