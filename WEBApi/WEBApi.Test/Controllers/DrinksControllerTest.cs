using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WEBApi.Controllers;
using WEBApi.Models;
using WEBApi.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WEBApi.Test.Controllers
{
    [TestFixture]
    public class DrinksControllerTest
    {
        private Mock<IDrinkService> _mockDrinkService;

        [SetUp]
        public void Startup()
        {
            _mockDrinkService = new Mock<IDrinkService>();
        }

        public List<Drink> CreateDrinksMockData()
        {
            List<Drink> mockList = new List<Drink>();

            mockList.Add(new Drink
            {
                Id = "001",
                DrinkName = "Italian Coffee",
                AviableNumbersOfDrink = 100,
                DrinkPrice = 2.5
            });
            mockList.Add(new Drink
            {
                Id = "002",
                DrinkName = "American Coffee",
                AviableNumbersOfDrink = 100,
                DrinkPrice = 2.3
            });
            mockList.Add(new Drink
            {
                Id = "003",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 50,
                DrinkPrice = 1.5
            });
            mockList.Add(new Drink
            {
                Id = "004",
                DrinkName = "Chocolate",
                AviableNumbersOfDrink = 80,
                DrinkPrice = 2
            });

            return mockList;
        }

        [Test]
        public void When_Ivoking_GetAllDrinks_It_Should_Return_All_Drinks()
        {
            //Arrange
            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetAllDrinks();

            //Assert
            _mockDrinkService.Verify(x => x.GetAll(), Times.Exactly(1));
            Assert.AreEqual(mockedResult.Count, result.Value.Count);
        }

        [Test]
        public void When_There_Is_No_Drink_GetAllDrinks_Mehtod_Should_Return_Null()
        {
            //Arrange
            List<Drink> mockedResult = null;

            _mockDrinkService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetAllDrinks();

            //Assert
            _mockDrinkService.Verify(x => x.GetAll(), Times.Exactly(1));
            Assert.IsNull(result.Value);
        }

        [Test]
        public void When_Invoking_GetById_It_Should_Return_Drink_With_The_Same_Id()
        {
            //Arrange
            string id = "002";
            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetDrink(id);

            //Assert
            _mockDrinkService.Verify(x => x.GetById(id), Times.Exactly(1));
            Assert.AreEqual(mockedResult[1].Id, result.Value.Id);
        }

        [Test]
        public void If_There_Is_No_Drink_With_The_Given_Id_GetDrink_Should_Return_NotFound()
        {
            //Arrange
            Drink drink = null;
            string id = "";

            _mockDrinkService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(drink);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetDrink(id);

            //Assert
            _mockDrinkService.Verify(x => x.GetById(It.IsAny<string>()), Times.Exactly(1));
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [Test]
        public void If_The_Id_Is_Null_GetDrink_Should_Return_BadRequest()
        {
            //Arrange
            string id = null;

            /*_mockDrinkService.Setup(x => x.GetById(id))
                .Returns(It.IsAny<Drink>());*/

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            try
            {
                controller.GetDrink(id);
            }
            catch (ArgumentNullException)
            {
                //Assert
                _mockDrinkService.Verify(x => x.GetById(id), Times.Exactly(0));
            }               
        }

        [Test]
        public void When_Invoking_CreateDrink_The_Create_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var drink = new Drink
            {
                Id = "003",
                DrinkName = "Azeri Tea",
                AviableNumbersOfDrink = 100,
                DrinkPrice = 3.00
            };

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            controller.CreateDrink(drink);

            //Assert
            _mockDrinkService.Verify(x => x.Create(drink), Times.Exactly(1));
        }


        [Test]
        public void When_Invoking_UpdateDrink_The_Update_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var newDrink = new Drink {
                Id = "003",
                DrinkName = "Azeri Tea",
                AviableNumbersOfDrink = 100,
                DrinkPrice = 3.00
            };

            _mockDrinkService.Setup(x => x.GetById(newDrink.Id))
                .Returns(newDrink);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.UpdateDrink(newDrink.Id, newDrink);
            

            //Assert
            _mockDrinkService.Verify(x => x.Update(newDrink.Id, newDrink), Times.Exactly(1));
            Assert.IsTrue(result is NoContentResult);
        }

        [Test]
        public void When_Invoking_UpdateDrink_With_Id_That_Does_Not_Exist_The_Update_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            Drink drink = new Drink
            {
                Id = "006",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.UpdateDrink(drink.Id, drink);

            //Assert
            _mockDrinkService.Verify(x => x.Update(drink.Id, drink), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_UpdateDrink_With_Invalid_Data_The_Update_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            var controller = new DrinksController(_mockDrinkService.Object);

            //Act

            //Assert
            _mockDrinkService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Drink>()), Times.Never);
        }

        [Test]
        public void When_Invoking_DeleteDrinkById_The_Remove_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            _mockDrinkService.Setup(x => x.GetById(drink.Id))
                .Returns(drink);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.DeleteDrinkById(drink.Id);


            //Assert
            _mockDrinkService.Verify(x => x.Remove(drink.Id), Times.Exactly(1));
            Assert.IsTrue(result is NoContentResult);
        }

        [Test]
        public void When_Invoking_DeleteDrinkById_With_Id_That_Does_Not_Exist_The_Remove_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            string id = "006";

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.DeleteDrinkById(id);

            //Assert
            _mockDrinkService.Verify(x => x.Remove(id), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_DeleteDrinkById_With_Invalid_Data_The_Remove_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            var controller = new DrinksController(_mockDrinkService.Object);

            //Act

            //Assert
            _mockDrinkService.Verify(x => x.Remove(It.IsAny<string>()), Times.Never);
        }
    }
}
