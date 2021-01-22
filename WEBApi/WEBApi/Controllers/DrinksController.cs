using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly ILogger<DrinksController> _logger;
        private readonly LinkGenerator _linkGenerator;
        DrinkManager drinkManager;

        public DrinksController(ILogger<DrinksController> logger, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _linkGenerator = linkGenerator;

            drinkManager = new DrinkManager();
            drinkManager.AddDrink("Italian coffee", 100, 2.50);
            drinkManager.AddDrink("American coffee", 90, 2.00);
            drinkManager.AddDrink("Tea", 100, 1.50);
            drinkManager.AddDrink("Tea", 80, 1.30);
            drinkManager.AddDrink("Chocolate", 100, 2.00);
        }

        [HttpGet]
        public List<Drink> GetDrinks()
        //public Task<ActionResult<List<Drink>>> GetDrinks()
        {
            try
            {
                var db = new TempDB();
                /* List<Drink> results = _mapper.Map<List<Drink>>(drinkManager.Drinks)
                return Ok();*/
                // return Ok(drinkManager.Drinks);
                //return drinkManager.Drinks;
                return db.drinks;
            }
            catch
            {
                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failer!");
            }
            return null;
            
        }

        [HttpGet("id/{drinkId}")]
        public List<Drink> GetDrink(int drinkId)
        {
            var result = new List<Drink>();

            foreach (var drink in drinkManager.Drinks)
            {
                if (drink.DrinkId == drinkId)
                    result.Add(drink);
            }

            if (result == null)
                return null;
                // return BadRequest();
   
            return result;
        }

        [HttpGet("drinkName/{drinkName}")]
        public List<Drink> GetDrinkByName(string drinkName)
        {
            var result = new List<Drink>();

            foreach (var drink in drinkManager.Drinks)
            {
                if (drink.DrinkName == drinkName)
                    result.Add(drink);
            }

            if (result == null)
                return null;
            // return BadRequest();

            return result;
        }

        [HttpGet("{drinkId}/price")]
        public List<double> GetDrinkPrice(int drinkId)
        {
            var result = new List<double>();

            foreach (var drink in drinkManager.Drinks)
            {
                if (drink.DrinkId == drinkId)
                    result.Add(drink.DrinkPrice);
            }

            if (result == null)
                return null;
            // return BadRequest();

            return result;
        }

        
        public Drink Post(Drink drink)
        {
            try
            {
                if (drinkManager.CheckDrink(drink.DrinkId))
                    return null;
                // return BadRequest("This drink is exists!");

                var location = _linkGenerator.GetPathByAction("Get", "Camps", new { drinkName = drink.DrinkName });
                if (string.IsNullOrWhiteSpace(location))
                    return null;
                //return BadRequest("Could not use current drinkId");

                drinkManager.AddDrink(drink.DrinkName, drink.AviableAmount, drink.DrinkPrice);

                //return Created($"/api/drinks/{drink.DrinkName}", drinkManager);
                return null;
            }
            catch (Exception)
            {
                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failer!");
            }
            return null;
            //return BadRequest();
        }

        [HttpPut("{drinkId}")]
        public List<Drink> Put(int drinkId, Drink newDrink)
        {
            try
            {
                if ( !drinkManager.CheckDrink(drinkId))
                    return null;
                // return BadRequest("There is no such a drink to modify!");


                for(int i=0; i < drinkManager.Drinks.Count(); i++)
                {
                    if (drinkManager.Drinks[i].DrinkId == drinkId)
                        drinkManager.Drinks[i] = newDrink;
                }

                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{drinkId}")]
        public List<Drink> Delete(int drinkId)
        {
            try
            {
                if (!drinkManager.CheckDrink(drinkId))
                    return null;
                // return BadRequest("There is no such a drink to delete!");


                for (int i = 0; i < drinkManager.Drinks.Count(); i++)
                {
                    if (drinkManager.Drinks[i].DrinkId == drinkId)
                        drinkManager.Drinks.Remove(new Drink() { DrinkId = drinkId });
                }

                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
