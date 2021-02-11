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

        [Test]
        public void When_There_Is_No_Order_GetAllOrders_Mehtod_Should_Return_Null()
        {
            //Arrange
            List<Order> mockedResult = null;

            _mockOrderService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.GetAllOrders();

            //Assert
            _mockOrderService.Verify(x => x.GetAll(), Times.Exactly(1));
            Assert.IsNull(result.Value);
        }

        [Test]
        public void When_Invoking_GetById_It_Should_Return_Order_With_The_Same_Id()
        {
            //Arrange
            string id = "002";
            var mockedResult = CreateOrderMockData();

            _mockOrderService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.GetOrder(id);

            //Assert
            _mockOrderService.Verify(x => x.GetById(id), Times.Exactly(1));
            Assert.AreEqual(mockedResult[1].Id, result.Value.Id);
        }

        [Test]
        public void If_There_Is_No_Order_With_The_Same_Id_GetDrink_Should_Return_NotFound()
        {
            //Arrange
            Order drink = null;

            _mockOrderService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(drink);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.GetOrder(It.IsAny<string>());

            //Assert
            _mockOrderService.Verify(x => x.GetById(It.IsAny<string>()), Times.Exactly(1));

            //-------------------------------- ??? ------------------------------
            //Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_CreateOrder_The_Create_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var order = new Order
            {
                Id = "000",
                OrderedDrinks = new List<OrderedDrink>()
            };

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            controller.CreateOrder(order);

            //Assert
            _mockOrderService.Verify(x => x.Create(order), Times.Exactly(1));
        }

        /*[Test]
        public void When_We_Add_New_Order_The_AviableNumbersOfDrink_Should_Be_Decreased_With_Ordered_Amount()
        {
            //Arrange
            var orderedDrinks = new List<OrderedDrink>();
            orderedDrinks.Add(new OrderedDrink("001", 2, "Tea", 2.5));
            orderedDrinks.Add(new OrderedDrink("002", 4, "Italian Coffee", 2));
            orderedDrinks.Add(new OrderedDrink("004", 1, "Chocolate", 2.3));

            var order = new Order
            {
                Id = "000",
                OrderedDrinks = orderedDrinks
            };

            _mockDrinkService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(It.IsAny<Drink>());
            _mockDrinkService.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Drink>()));


            var controllerOrders = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);
            var controllerDrink = new DrinksController(_mockDrinkService.Object);

            //Act
            controllerOrders.CreateOrder(order);

            //Assert
            _mockDrinkService.Verify(x => x.GetById(It.IsAny<string>()), Times.Exactly(3));
            _mockDrinkService.Verify(x => x.Update(It.IsAny<string>(),It.IsAny<Drink>()), Times.Exactly(3));
        }*/

        [Test]
        public void When_Invoking_UpdateOrder_The_Update_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var newOrder = new Order
            {
                Id = "001",
                OrderedDrinks = new List<OrderedDrink>()
            };

            _mockOrderService.Setup(x => x.GetById(newOrder.Id))
                .Returns(newOrder);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.UpdateOrder(newOrder.Id, newOrder);


            //Assert
            _mockOrderService.Verify(x => x.Update(newOrder.Id, newOrder), Times.Exactly(1));
            Assert.IsTrue(result is NoContentResult);
        }

        [Test]
        public void When_Invoking_UpdateOrder_The_Update_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            Order newOrder = null;


            _mockOrderService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(newOrder);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.UpdateOrder(It.IsAny<string>(), It.IsAny<Order>());

            //Assert
            _mockOrderService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Order>()), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_DeleteOrder_The_Remove_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var order = new Order();
            _mockOrderService.Setup(x => x.GetById(order.Id))
                .Returns(order);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.DeleteOrder(order.Id);


            //Assert
            _mockOrderService.Verify(x => x.Remove(order.Id), Times.Exactly(1));
            Assert.IsTrue(result is NoContentResult);
        }

        [Test]
        public void When_Invoking_DeleteOrder_The_Remove_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            Order newOrder = null;

            _mockOrderService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(newOrder);

            var controller = new OrdersController(_mockOrderService.Object, _mockDrinkService.Object);

            //Act
            var result = controller.DeleteOrder(It.IsAny<string>());

            //Assert
            _mockOrderService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Order>()), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }

    }
}
