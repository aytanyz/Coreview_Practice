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
        public void When_There_Is_No_Drink_GetAllDrink_Mehtod_Should_Return_Null()
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
        public void If_There_Is_No_Drink_With_The_Same_Id_GetDrink_Should_Return_NotFound()
        {
            //Arrange
            Drink drink = null;

            _mockDrinkService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(drink);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetDrink(It.IsAny<string>());

            //Assert
            _mockDrinkService.Verify(x => x.GetById(It.IsAny<string>()), Times.Exactly(1));
            
            //-------------------------------- ??? ------------------------------
            //Assert.IsTrue(result is NotFoundResult);
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
        public void When_Invoking_UpdateDrink_The_Update_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            Drink newDrink = null;


            _mockDrinkService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(newDrink);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.UpdateDrink(It.IsAny<string>(), It.IsAny<Drink>());

            //Assert
            _mockDrinkService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Drink>()), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_DeleteDrinkById_The_Remove_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var drink = new Drink();
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
        public void When_Invoking_DeleteDrinkById_The_Remove_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            Drink newDrink = null;

            _mockDrinkService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(newDrink);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.DeleteDrinkById(It.IsAny<string>());

            //Assert
            _mockDrinkService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Drink>()), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }
    }
}
