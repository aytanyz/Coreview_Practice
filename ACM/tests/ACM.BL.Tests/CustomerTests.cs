using System;
using Xunit;

namespace ACM.BL.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void FullNameValid()
        {
            // arrange
            Customer customer = new Customer
            {
                FirstName = "Bilbo",
                LastName = "Beggins"
            };
            string expected = "Bilbo, Beggins";

            // act
            string actual = customer.FullName;

            // assert
            //Assert.Equal("hh", "hh");
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void StaticTest()
        {
            //arrange
            var c1 = new Customer();
            c1.FirstName = "Bilbo";
            Customer.InstanceCount += 1;

            var c2 = new Customer();
            c2.FirstName = "Frodo";
            Customer.InstanceCount += 1;

            var c3 = new Customer();
            c3.FirstName = "Rosie";
            Customer.InstanceCount += 1;

            //act

            //assert
            Assert.Equal(3, Customer.InstanceCount);
        }

        [Fact]
        public void ValidateValidTest()
        {
            //arrange
            var customer = new Customer
            {
                LastName = "Baggins",
                EmailAddress = "fb@gmail.com"
            };

            var expected = true;

            //act
            var actual = customer.Validate();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateMissingLastNameTest()
        {
            //arrange
            var customer = new Customer
            {
                EmailAddress = "fb@gmail.com"
            };

            var expected = false;

            //act
            var actual = customer.Validate();

            //assert
            Assert.Equal(expected, actual);
        }
    }
}
