using System;
using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public class EFRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateById(int id, Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}
