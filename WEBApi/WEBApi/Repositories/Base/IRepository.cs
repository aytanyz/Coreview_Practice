using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Repositories.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        public List<T> GetAll();

        public T GetById(string id);

        public void Create(T drink);

        public void Update(string id, T newItem);

        public void Remove(string id);
    }
}
