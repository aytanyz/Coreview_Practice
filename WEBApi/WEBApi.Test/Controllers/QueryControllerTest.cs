using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WEBApi.Controllers;
using WEBApi.Models;
using WEBApi.Services;
using System.Linq;

namespace WEBApi.Test.Controllers
{
    [TestFixture]
    public class QueryControllerTest
    {
        private Mock<IDrinkService> _mockDrinkService;
        private Mock<IOrderService> _mockOrderService;

        [SetUp]
        public void Startup()
        {
            _mockDrinkService = new Mock<IDrinkService>();
            _mockOrderService = new Mock<IOrderService>();
        }

        public List<Drink> CreateDrinksMockData()
        {
            List<Drink> mockList = new List<Drink>();

            mockList.Add(new Drink("Italian Coffee", 100, 2.5));
            mockList[0].Id = "001";
            mockList.Add(new Drink("American Coffee", 100, 2.3));
            mockList[1].Id = "002";
            mockList.Add(new Drink("Tea", 50, 1.5));
            mockList[2].Id = "003";
            mockList.Add(new Drink("Chocolate", 80, 2));
            mockList[3].Id = "004";

            return mockList;
        }

        public List<Order> CreateordersMockData()
        {
            var mockList = new List<Order>();
            List<OrderedDrink> orderedDrink = new List<OrderedDrink>();

            orderedDrink.Add(new OrderedDrink("001", 2, "Italian Coffee", 2.5));
            orderedDrink.Add(new OrderedDrink("002", 2, "American Coffee", 2.3));
            mockList.Add(new Order(orderedDrink, null));
            mockList[0].Id = "001";
            orderedDrink.RemoveAll(x => true);


            orderedDrink.Add(new OrderedDrink("002", 4, "American Coffee", 2.3));
            mockList.Add(new Order(orderedDrink, null));
            mockList[1].Id = "002";
            orderedDrink.RemoveAll(x => true);

            orderedDrink.Add(new OrderedDrink("001", 3, "Italian Coffee", 2.5));
            orderedDrink.Add(new OrderedDrink("004", 3, "Chocolate", 2));
            mockList.Add(new Order(orderedDrink, null));
            mockList[2].Id = "003";
            orderedDrink.RemoveAll(x => true);


            orderedDrink.Add(new OrderedDrink("003", 1, "Tea", 1.5));
            orderedDrink.Add(new OrderedDrink("004", 1, "Chocolate", 2));
            mockList.Add(new Order(orderedDrink, null));
            mockList[3].Id = "004";
            orderedDrink.RemoveAll(x => true);

            return mockList;
        }

        [Test]
        public void GetAllDrinksWhereNumbersOfDrinkIsLessThan_Test()
        {

        }


    }
}
