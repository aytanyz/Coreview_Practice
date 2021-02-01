
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WEBApi.Models;
using WEBApi.Services;

namespace WEBApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ResetDrinksController : ControllerBase
	{
		private readonly DrinkService _drinkService;
		public ResetDrinksController(DrinkService drinkService)
		{
			_drinkService = drinkService;
		}


		[HttpGet]
		public ActionResult<List<Drink>> ResetCollection()
		{
			// delete collection
			List<Drink> drinks = _drinkService.GetAllDrinks();
			foreach (var drink in drinks)
				_drinkService.RemoveDrinkById(drink.Id);

			// read drinks from file
			var readFile = new ReadDrinksFromCSV();
			drinks = readFile.ProcessFile("Files/Drinks.csv");


			// add drinks to collection
			foreach (var drink in drinks)
			{
				_drinkService.CreateDrink(drink);
				CreatedAtRoute("GetDrink", new { id = drink.Id.ToString() }, drink);
			}

			return _drinkService.GetAllDrinks();
		}
	}
}


