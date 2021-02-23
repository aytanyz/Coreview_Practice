using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WEBApi.Controllers;
using WEBApi.Models;
using WEBApi.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using WEBApi.Models.Validators;

namespace WEBApi.Test.Controllers
{
    [TestFixture]
    class DrinkValidatorTest
    {
        [Test]
        public void If_No_Rule_Was_Broken_No_Error_Message_Should_Occur()
        {
            //Arrange
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);

            //Assert
            Assert.AreEqual(0, resultValidator.Errors.Count);
        }

        [Test]
        public void If_Id_is_Null()
        {
            //Arrange
            string expectedErrorMessage = "Id is null."; Drink drink = new Drink
            {
                Id = null,
                DrinkName = "Tea",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.Id), resultValidator.Errors[0].PropertyName);
        }

        
        [Test]
        public void If_DrinkName_is_Null()
        {
            //Arrange
            string expectedErrorMessage = "Drink Name is null.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = null,
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.DrinkName), resultValidator.Errors[0].PropertyName);
        }

        [Test]
        public void If_DrinkName_length_is_LessThan_2()
        {
            //Arrange
            string expectedErrorMessage = "Drink Name has invalid length.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.DrinkName), resultValidator.Errors[0].PropertyName);
        }

        [Test]
        public void If_DrinkName_length_is_2_No_ErrorMessage_Should_Occur()
        {
            //Arrange
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Su",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);

            //Assert
            Assert.AreEqual(0, resultValidator.Errors.Count);
        }

        [Test]
        public void If_DrinkName_length_is_GreaterThan_50()
        {
            //Arrange
            string expectedErrorMessage = "Drink Name has invalid length.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "123456789012345678901234567890123456789012345678901",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.DrinkName), resultValidator.Errors[0].PropertyName);
        }

        [Test]
        public void If_DrinkName_length_is_50_No_ErrorMessage_Should_Occur()
        {
            //Arrange
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "12345678901234567890123456789012345678901234567890",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);

            //Assert
            Assert.AreEqual(0, resultValidator.Errors.Count);
        }

        [Test]
        public void If_DrinkPrice_is_LessThan_0()
        {
            //Arrange
            string expectedErrorMessage = "Drink Price is invalid.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 20,
                DrinkPrice = -1.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.DrinkPrice), resultValidator.Errors[0].PropertyName);
        }

        [Test]
        public void If_DrinkPrice_is_Equal_0()
        {
            //Arrange
            string expectedErrorMessage = "Drink Price is invalid.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 20,
                DrinkPrice = 0
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.DrinkPrice), resultValidator.Errors[0].PropertyName);
        }

        [Test]
        public void If_AviableNumbersOfDrink_is_LessThan_0()
        {
            //Arrange
            string expectedErrorMessage = "Aviable Numbers Of Drink is invalid.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Tea",
                AviableNumbersOfDrink = -1,
                DrinkPrice = 2.5
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.AviableNumbersOfDrink), resultValidator.Errors[0].PropertyName);
        }

        [Test]
        public void If_AviableNumbersOfDrink_is_Equal_0()
        {
            //Arrange
            string expectedErrorMessage = "Aviable Numbers Of Drink is invalid.";
            Drink drink = new Drink
            {
                Id = "001",
                DrinkName = "Tea",
                AviableNumbersOfDrink = 0,
                DrinkPrice = 3
            };

            //Act
            var drinkValidator = new DrinkValidator();
            var resultValidator = drinkValidator.Validate(drink);
            string actualErrorMessage = resultValidator.Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(nameof(drink.AviableNumbersOfDrink), resultValidator.Errors[0].PropertyName);
        }
    }
}
