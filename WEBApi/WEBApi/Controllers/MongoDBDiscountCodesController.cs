using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Models;
using WEBApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace WEBApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MongoDBDiscountCodesController : ControllerBase
    {
        private readonly DiscountCodeService _discountCodeService;
        
        public MongoDBDiscountCodesController(DiscountCodeService discountCodeService)
        {
            _discountCodeService = discountCodeService;
        }

		[HttpGet]
		public ActionResult<List<DiscountCode>> GetAllDrinks() =>
			_discountCodeService.GetAllDiscountCodes();

		//[HttpGet("{id:length(24)}", Name = "GetDrink")]
		[HttpGet("{id}", Name = "GetDiscountCode")]
		public ActionResult<DiscountCode> Get(string id)
		{
			var discountCode = _discountCodeService.GetDiscountCodeById(id);

			if (discountCode == null)
			{
				return NotFound();
			}

			return discountCode;
		}

		[HttpPost]
		public ActionResult<DiscountCode> CreateDiscountCode(DiscountCode discountCode)
		{
			_discountCodeService.Create(discountCode);

			return CreatedAtRoute("GetDrink", new { id = discountCode.Id.ToString() }, discountCode);
		}

		// ---------------------add from file------------------------------
		[HttpPost("fromfile")]
		public IActionResult AddFromFile()
		{
			var readFile = new ReadDiscountCodesFromCSV();
			List <DiscountCode> discountCodes = readFile.ProcessFile("Files/DiscountCodes.csv");

			foreach (var discountCode in discountCodes)
			{
				_discountCodeService.Create(discountCode);
				CreatedAtRoute("GetDrink", new { id = discountCode.Id.ToString() }, discountCode);
			}

			return NoContent();
		}
		// ----------------------------------------------------------------

		[HttpPut("{id:length(24)}")]
		public IActionResult Update(string id, DiscountCode newDiscountCode)
		{
			var book = _discountCodeService.GetDiscountCodeById(id);

			if (book == null)
			{
				return NotFound();
			}

			_discountCodeService.Update(id, newDiscountCode);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var discountCode = _discountCodeService.GetDiscountCodeById(id);

			if (discountCode == null)
			{
				return NotFound();
			}

			_discountCodeService.RemoveById(discountCode.Id);

			return NoContent();
		}
	}
}
