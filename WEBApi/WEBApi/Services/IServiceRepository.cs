using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Services
{
    public interface IServiceRepository<T>
    {
        public List<T> GetAll();
        public T GetById(string id);
        public T Create(T item);
        public void Update(string id, T item);
        public void Remove(string id);

    }
}
