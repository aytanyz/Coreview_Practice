using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WEBApi.Controllers;
using WEBApi.Models;
using WEBApi.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace WEBApi.Test.Controllers
{
    [TestFixture]
    public class OrdersControllerTest
    {
        public Mock<IOrderService> _mockOrderService;
        private Mock<IDrinkService> _mockDrinkService;

        [SetUp]
        public void Startup()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockDrinkService = new Mock<IDrinkService>();
        }

        public List<Order> CreateOrderMockData()
        {
            var mockList = new List<Order>();
            var orderedDrinks = new List<OrderedDrink>();

            orderedDrinks.Add(new OrderedDrink
            {
                DrinkId = "001",
                DrinkName = "Italian Coffee",
                NumbersOfDrink = 2,
                DrinkPrice = 2.5
            });
            orderedDrinks.Add(new OrderedDrink
            {
                DrinkId = "003",
                DrinkName = "Tea",
                NumbersOfDrink = 2,
                DrinkPrice = 1.5
            });
            mockList.Add(new Order
            {
                Id = "001",
                OrderedDrinks = orderedDrinks,
                DiscountCodeId = null
            }) ;
            orderedDrinks.RemoveAll(x => true);


            orderedDrinks.Add(new OrderedDrink
            {
                DrinkId = "002",
                DrinkName = "American Coffee",
                NumbersOfDrink = 5,
                DrinkPrice = 2.3
            });
            orderedDrinks.Add(new OrderedDrink
            {
                DrinkId = "003",
                DrinkName = "Tea",
                NumbersOfDrink = 1,
                DrinkPrice = 1.5
            });
            mockList.Add(new Order
            {
                Id = "002",
                OrderedDrinks = orderedDrinks,
                DiscountCodeId = null
            });
            orderedDrinks.RemoveAll(x => true);

            return mockList;
        }

        [Test]
        public void When_Ivoking_GetAllOrders_It_Should_Return_All_Orders()
        { 
            //Arrange
            var mockedResult = CreateOrderMockData();

            _mockOrderService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.GetAllOrders();

            //Assert
            _mockOrderService.Verify(x => x.GetAll(), Times.Exactly(1));
            Assert.AreEqual(mockedResult.Count, result.Value.Count);
        }

    }
}
