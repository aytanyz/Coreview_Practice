using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WEBApi.Models;
using WEBApi.Models.Validators;
using WEBApi.Services;

namespace WEBApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class DrinksController : ControllerBase
    {
		private readonly IDrinkService _drinkService;			

		public DrinksController(IDrinkService drinkService)
        {
			_drinkService = drinkService;
		}

		[HttpGet("error_test")]
		public ActionResult<Drink> ErrorTest()
		{
			throw new Exception("testing execution");
		}

		[HttpGet]
		public ActionResult<List<Drink>> GetAllDrinks() =>
			_drinkService.GetAll();


		[HttpGet("{id:length(24)}", Name = "GetDrink")]
		public ActionResult<Drink> GetDrink(string id)
		{
			var idValidator = new IdValidator();
			var resultIdValidator = idValidator.Validate(id);

			if (!resultIdValidator.IsValid)
				return BadRequest();

			var drink = _drinkService.GetById(id);

			if (drink == null)
			{
				return NotFound();
			}

			return drink;
		}

		[HttpPost]
		public ActionResult<Drink> CreateDrink(Drink drink)
		{
			var drinkValidator = new DrinkValidator();
			var resultValidator = drinkValidator.Validate(drink);

			if (!resultValidator.IsValid)
				return BadRequest();

			_drinkService.Create(drink);
			return CreatedAtRoute("GetDrink", new { id = drink.Id.ToString() }, drink);
		}

		// ---------------------add from file------------------------------
		//[HttpPost("fromfile")]
		//public IActionResult AddFromFile()
		//{
		//	var readFile = new ReadDrinksFromCSV("Files/Drinks.csv");
		//	List<Drink> drinks = readFile.drinks;

		//	foreach (var drink in drinks)
		//	{
		//		_drinkService.Create(drink);
		//		CreatedAtRoute("GetDrink", new { id = drink.Id.ToString() }, drink);
		//	}

		//	return NoContent();
		//}
		// ----------------------------------------------------------------


		[HttpPut("{id:length(24)}")]
		public IActionResult UpdateDrink(string id, Drink newDrink)
		{
			newDrink.Id = id;

			var drinkValidator = new DrinkValidator();
			var resultValidator = drinkValidator.Validate(newDrink);

			if (!resultValidator.IsValid)
				return BadRequest();

			var drink = _drinkService.GetById(id);

			if (drink == null)
			{
				return NotFound();
			}

			_drinkService.Update(id, newDrink);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult DeleteDrinkById(string id)
		{
			var idValidator = new IdValidator();
			var resultValidator = idValidator.Validate(id);

			if (!resultValidator.IsValid)
				return BadRequest();

			var drink = _drinkService.GetById(id);

			if (drink == null)
			{
				return NotFound();
			}

			_drinkService.Remove(drink.Id);

			return NoContent();
		}
	}	
}




/*
namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksMangoDBController : ControllerBase 
    {
		private readonly IRepositoryUser _repositoryUser;
		private readonly IRepositoryAuthor _repositoryAuthor;
		private readonly IClientSessionHandle _clientSessionHandle;

		public DrinksMangoDBController(
			IRepositoryUser repositoryUser,
			IRepositoryAuthor repositoryAuthor,
			IClientSessionHandle clientSessionHandle) =>
					(_repositoryUser, _repositoryAuthor, _clientSessionHandle) =
						(repositoryUser, repositoryAuthor, clientSessionHandle);
	

		[HttpPost]
		[Route("user")]
		public async Task<IActionResult> InsertUser([FromBody] CreateUserModel userModel)
		{
			_clientSessionHandle.StartTransaction();

			try
			{
				var user = new User(userModel.Name, userModel.Nin);
				await _repositoryUser.InsertAsync(user);
				await _clientSessionHandle.CommitTransactionAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				await _clientSessionHandle.AbortTransactionAsync();

				return BadRequest(ex);
			}
		}

		[HttpPost]
		[Route("authorAndUser")]
		public async Task<IActionResult> InsertAuthorAndUser([FromBody] CreateAuthorAndUserModel authorAndUserModel)
		{
			_clientSessionHandle.StartTransaction();

			try
			{
				var author = new Author(authorAndUserModel.AuthorModel.Name, new List<Book>(authorAndUserModel.AuthorModel.Books.Select(s => new Book(s.Name, s.Year))));
				var user = new User(authorAndUserModel.UserModel.Name, authorAndUserModel.UserModel.Nin);

				await _repositoryAuthor.InsertAsync(author);
				await _repositoryUser.InsertAsync(user);
				await _clientSessionHandle.CommitTransactionAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				await _clientSessionHandle.AbortTransactionAsync();

				return BadRequest(ex);
			}
		}

	}
}*/
