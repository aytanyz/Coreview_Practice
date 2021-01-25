using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly ILogger<DrinksController> _logger;
        private readonly LinkGenerator _linkGenerator;

        public DrinksController(ILogger<DrinksController> logger, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public List<Drink> GetAllDrinks()
        {
            try
            {
                return TempDB.drinks;
            }
            catch
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }         
        }

        [HttpGet("id/{drinkId}")]
        public List<Drink> GetDrink(int drinkId)
        {

            var result = new List<Drink>();
            try
            {
                foreach (var drink in TempDB.drinks)
                {
                    if (drink.DrinkId == drinkId)
                        result.Add(drink);
                }

                if (result == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            catch
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            return result;
        }

        [HttpGet("drinkName/{drinkName}")]
        public List<Drink> GetDrinkByName(string drinkName)
        {
            var result = new List<Drink>();

            try
            {
                foreach (var drink in TempDB.drinks)
                {
                    if (drink.DrinkName == drinkName)
                        result.Add(drink);
                }

                if (result == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            catch
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            return result;
        }

        [HttpGet("{drinkId}/price")]
        public List<double> GetDrinkPrice(int drinkId)
        {
            var result = new List<double>();

            try
            {
                foreach (var drink in TempDB.drinks)
                {
                    if (drink.DrinkId == drinkId)
                        result.Add(drink.DrinkPrice);
                }

                if (result == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            catch
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            return result;
        }

        [HttpPost]
        public Drink Post(Drink drink)
        {
            try
            {
                TempDB.drinkManager.AddDrink(drink.DrinkName, drink.AviableAmount, drink.DrinkPrice);

                return TempDB.drinks[TempDB.drinks.Count-1];
            }
            catch
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPut("{drinkId}")]
        public Drink Put(int drinkId, Drink newDrink)
        {
            try
            {
                int editedDrinkId = -1;
                for(int i=0; i < TempDB.drinks.Count(); i++)
                {
                    if (TempDB.drinks[i].DrinkId == drinkId)
                    {
                        TempDB.drinks[i] = newDrink;
                        editedDrinkId = i;
                    }
                }
                return TempDB.drinks[editedDrinkId];

            }
            catch (Exception)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }

        [HttpDelete("id/{drinkId}")]
        public List<Drink> DeleteDrink(int drinkId)
        {
            try
            {
                if (!TempDB.drinkManager.CheckDrink(drinkId))
                    return null;
                // return BadRequest("There is no such a drink to delete!");

                Drink item = TempDB.drinks.Single(x => x.DrinkId == drinkId);
                TempDB.drinks.Remove(item);

                /*for (int i = 0; i < TempDB.drinks.Count(); i++)
                {
                    if (TempDB.drinks[i].DrinkId == drinkId)
                        TempDB.drinks.RemoveAt(i);
                }*/

                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
