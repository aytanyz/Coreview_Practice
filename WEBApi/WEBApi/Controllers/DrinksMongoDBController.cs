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
	public class DrinksMongoDBController : ControllerBase
    {
		private readonly DrinkService _drinkService;
		public DrinksMongoDBController(DrinkService drinkService)
		{
			_drinkService = drinkService;	
		}

		[HttpGet]
		public ActionResult<List<Drink>> GetAllDrinks() =>
			_drinkService.GetAllDrinks();

		//[HttpGet("{id:length(24)}", Name = "GetDrink")]
		[HttpGet("{id}", Name = "GetDrink")]
		public ActionResult<Drink> Get(string id)
		{
			var drink = _drinkService.GetDrinkById(id);

			if (drink == null)
			{
				return NotFound();
			}

			return drink;
		}

		[HttpPost]
		public ActionResult<Drink> Create(Drink drink)
		{
			_drinkService.Create(drink);

			return CreatedAtRoute("GetDrink", new { id = drink.Id.ToString() }, drink);
		}

		[HttpPut("{id:length(24)}")]
		public IActionResult Update(string id, Drink newDrink)
		{
			var book = _drinkService.GetDrinkById(id);

			if (book == null)
			{
				return NotFound();
			}

			_drinkService.Update(id, newDrink);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var book = _drinkService.GetDrinkById(id);

			if (book == null)
			{
				return NotFound();
			}

			_drinkService.RemoveById(book.Id);

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
