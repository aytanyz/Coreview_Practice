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

        [Test]
        public void GetAllDrinks_Test()
        {
            //Arrange
            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetAllDrinks();

            //Assert
            Assert.AreEqual(mockedResult.Count, result.Value.Count);
            Assert.AreEqual(mockedResult.FirstOrDefault().Id, result.Value.FirstOrDefault().Id);
            Assert.AreEqual(mockedResult[^1].Id, result.Value[^1].Id);
        }

        [Test]
        public void GetAllDrinks_HasNothing()
        {
            //Arrange
            var mockedResult = new List<Drink>();

            _mockDrinkService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.GetAllDrinks();

            //Assert
            Assert.AreEqual(mockedResult.Count, result.Value.Count);
            Assert.IsNull(result.Value.FirstOrDefault());
        }

        [Test]
        public void GetDrinkById_Test()
        {
            //Arrange
            string id = "002";
            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.Get(id);

            //Assert
            Assert.AreEqual(mockedResult[1].DrinkName, result.Value.DrinkName);
        }

        [Test]
        public void GetDrinkById_NoDrinkWithGivenID()
        {
            //Arrange
            string id = "000";
            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            var result = controller.Get(id);

            //Assert
            Assert.IsNull(result.Value);
        }

        [Test]
        public void Create_Test()
        {
            //Arrange
            var drink = new Drink("Azeri Tea", 100, 3.00);
            drink.Id = "005";

            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.Create(drink));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            controller.CreateDrink(drink);

            //Assert
            _mockDrinkService.Verify(x => x.Create(drink), Times.Exactly(1));
        }


        [Test]
        public void Update_Test()
        {
            //Arrange
            var newDrink = new Drink("Azeri Tea", 100, 3.00);
            string id = "003";

            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.Update(id, newDrink));
            _mockDrinkService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            controller.UpdateDrink(id, newDrink);
            var result = controller.Get(id);

            //Assert
            _mockDrinkService.Verify(x => x.Update(id, newDrink), Times.Exactly(1));
            //Assert.AreEqual(newDrink.DrinkName, result.Value.DrinkName);
            //Assert.AreEqual(newDrink.AviableNumbersOfDrink, result.Value.AviableNumbersOfDrink);
            //Assert.AreEqual(newDrink.DrinkPrice, result.Value.DrinkPrice);
        }

        [Test]
        public void Update_IfNoDrinkWithGivenId()
        {
            //Arrange
            var newDrink = new Drink("Azeri Tea", 100, 3.00);
            string id = "000";

            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.Update(id, newDrink));
            _mockDrinkService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            controller.UpdateDrink(id, newDrink);
            var result = controller.Get(id);

            //Assert
            _mockDrinkService.Verify(x => x.Update(id, newDrink), Times.Exactly(0));
        }

        [Test]
        public void Remove_Test()
        {
            //Arrange
            string id = "001";

            var mockedResult = CreateDrinksMockData();

            _mockDrinkService.Setup(x => x.Remove(id));
            _mockDrinkService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DrinksController(_mockDrinkService.Object);

            //Act
            controller.DeleteDrinkById(id);
            var result = controller.Get(id);

            //Assert
            _mockDrinkService.Verify(x => x.Remove(id), Times.Exactly(1));
            //Assert.IsNull(result.Value);
        }

    }
}
