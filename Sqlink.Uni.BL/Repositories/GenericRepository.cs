using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Sqlink.Uni.BL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private ConcurrentDictionary<int, T> _items;
        private List<Action> _actionList;

        public GenericRepository(InMemoryDb inMemoryDb)
        {
            _items = inMemoryDb.GetConcurrentDictionary<T>();
            _actionList = new List<Action>();
        }
      
        public IEnumerable<T> GetAll()
        {
            return _items.Values.AsEnumerable();
        }
        public T GetById(int id)
        {
            var obj = _items[id];
            return obj;
        }
        public void Insert(T obj)
        {
            _actionList.Add(() =>
            {
                var id = 0;// _items.Any() ? 0 : _items.Keys.Max(m => m);
                if(_items.Any())
                {
                    id = _items.Keys.Max(m => m);
                }
                id++;

                obj.Id = id;
                _items.TryAdd(id, obj);
            });
          
        }
        
        public void Delete(int id)
        {
            _actionList.Add(() =>
            {
                _items.Remove(id, out T value);
            });
           
        }
        public void Save()
        {
            foreach (var action in _actionList)
            {
                action();
            }
            _actionList.Clear();
        }

        public void UpdateById(int id, Action<T> action)
        {
            _actionList.Add(() =>
            {
                var obj = GetById(id);
                action(obj);
            });
        }
    }


}
