using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBApi.Models;

namespace WEBApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DrinksDBContextController : ControllerBase
    {
        private readonly DrinkContext _context;

        public DrinksDBContextController(DrinkContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetAllDrinks()
        {
            return await _context.Drinks.ToListAsync();
        }

        
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Drink>> GetDrinkById(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return drink;
        }
       
        [HttpPut("{id}")]
        public async Task<ActionResult<Drink>> PutDrink(int id, Drink drink)
        {
            if(drink.DrinkId == 0)
                drink.DrinkId = id;

            _context.Entry(drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
             //return await GetDrinkById(id);
        }

        
        [HttpPost]
        public async Task<ActionResult<Drink>> PostDrink(Drink drink)
        {
            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.DrinkId }, drink);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drink>> DeleteDrink(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return drink;
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.DrinkId == id);
        }
    }
}
