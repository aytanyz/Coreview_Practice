using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;

namespace WEBApi.Services
{
    public interface IDrinkService
    {

        public List<Drink> GetAll();

        public Drink GetById(string id);

        public Drink Create(Drink item);

        public void Update(string id, Drink item);

        public void Remove(string id);
    }
}
