using System.Collections.Generic;
using WEBApi.Models;
using WEBApi.Repositories.Drinks;

namespace WEBApi.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinksRepository _drinksRepository;

        public DrinkService(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public List<Drink> GetAll() =>
            _drinksRepository.GetAll();

        public Drink GetById(string id) =>
            _drinksRepository.GetById(id);

        public void Create(Drink drink)
        {
            _drinksRepository.Create(drink);
        }

        public void Update(string id, Drink newDrink) =>
            _drinksRepository.Update(id, newDrink);

        public void Remove(string id) =>
            _drinksRepository.Remove(id);

    }
}
