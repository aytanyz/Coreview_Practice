using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Services
{
    public interface IDrinkService
    {
        public List<Drink> GetAll();

        public Drink GetById(string id);

        public void Create(Drink item);

        public void Update(string id, Drink item);

        public void Remove(string id);
    }
}
