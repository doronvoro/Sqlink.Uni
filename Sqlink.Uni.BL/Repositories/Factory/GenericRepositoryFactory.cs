using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlink.Uni.BL
{
    public class GenericRepositoryFactory<T> : IGenericRepositoryFactory<T> where T : BaseEntity
    {
        private RepositoryStorageOptions _storageOptions;
        private IEnumerable<IGenericRepository<T>> _genericRepositories;

        public GenericRepositoryFactory(IOptionsSnapshot<RepositoryStorageOptions>   optionsSnapshot , 
            IEnumerable<IGenericRepository<T>> genericRepositories)
        {
            _storageOptions = optionsSnapshot.Value;
            _genericRepositories = genericRepositories;
        }

        public IGenericRepository<T> GetRepository()
        {
            if (_storageOptions.Name == "InMemory")
            {
                return _genericRepositories.FirstOrDefault(f => f.GetType() == typeof(InMemoryRepository<T>));
            }
            else if (_storageOptions.Name == "EF")
            {
                return _genericRepositories.FirstOrDefault(f => f.GetType() == typeof(EFRepository<T>));
            }
            throw new InvalidOperationException();
        }

       
    }


}
