using System;
using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void UpdateById(int id, Action<T> action);
        void Delete(int id);
        void Save();
    }


}
