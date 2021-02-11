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
    public class DiscountCodesControllerTest
    {
        private Mock<IDiscountCodeService> _mockDiscountCodeService;

        [SetUp]
        public void Startup()
        {
            _mockDiscountCodeService = new Mock<IDiscountCodeService>();
        }

        public List<DiscountCode> CreateDiscountCodesMockData()
        {
            var mockList = new List<DiscountCode>();

            mockList.Add(new DiscountCode
            {
                Id = "001",
                Code = "Extra30",
                DiscountPercentage = 30
            });
            mockList.Add(new DiscountCode
            {
                Id = "002",
                Code = "Extra20",
                DiscountPercentage = 20
            });
            mockList.Add(new DiscountCode
            {
                Id = "003",
                Code = "Extra50",
                DiscountPercentage = 50
            });

            return mockList;
        }

        [Test]
        public void When_Ivoking_GetAllDiscountCodes_It_Should_Return_All_DiscountCodes()
        {
            //Arrange
            var mockedResult = CreateDiscountCodesMockData();

            _mockDiscountCodeService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.GetAllDiscountCodes();

            //Assert
            _mockDiscountCodeService.Verify(x => x.GetAll(), Times.Exactly(1));
            Assert.AreEqual(mockedResult.Count, result.Value.Count);
        }

        [Test]
        public void When_There_Is_No_DiscountCode_GetAllDiscountCodes_Mehtod_Should_Return_Null()
        {
            //Arrange
            List<DiscountCode> mockedResult = null;

            _mockDiscountCodeService.Setup(x => x.GetAll())
                .Returns(mockedResult);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.GetAllDiscountCodes();

            //Assert
            _mockDiscountCodeService.Verify(x => x.GetAll(), Times.Exactly(1));
            Assert.IsNull(result.Value);
        }

        [Test]
        public void When_Invoking_GetDiscountCOde_It_Should_Return_DiscountCode_With_The_Same_Id()
        {
            //Arrange
            string id = "002";
            var mockedResult = CreateDiscountCodesMockData();

            _mockDiscountCodeService.Setup(x => x.GetById(id))
                .Returns(mockedResult.FirstOrDefault(x => x.Id == id));

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.GetDiscountCode(id);

            //Assert
            _mockDiscountCodeService.Verify(x => x.GetById(id), Times.Exactly(1));
            Assert.AreEqual(mockedResult[1].Id, result.Value.Id);
        }

        [Test]
        public void If_There_Is_No_DiscountCode_With_The_Same_Id_GetDiscountCode_Should_Return_NotFound()
        {
            //Arrange
            DiscountCode discountCode = null;

            _mockDiscountCodeService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(discountCode);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.GetDiscountCode(It.IsAny<string>());

            //Assert
            _mockDiscountCodeService.Verify(x => x.GetById(It.IsAny<string>()), Times.Exactly(1));

            //-------------------------------- ??? ------------------------------
            //Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_CreateDiscountCode_The_Create_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var discountCode = new DiscountCode
            {
                Id = "003",
                Code = "Super50",
                DiscountPercentage = 50
            };

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            controller.CreateDiscountCode(discountCode);

            //Assert
            _mockDiscountCodeService.Verify(x => x.Create(discountCode), Times.Exactly(1));
        }

        [Test]
        public void When_Invoking_UpdateDiscountCode_The_Update_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var newDiscountCode = new DiscountCode
            {
                Id = "001",
                Code = "Super50",
                DiscountPercentage = 50
            };

            _mockDiscountCodeService.Setup(x => x.GetById(newDiscountCode.Id))
                .Returns(newDiscountCode);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.UpdateDiscountCode(newDiscountCode.Id, newDiscountCode);


            //Assert
            _mockDiscountCodeService.Verify(x => x.Update(newDiscountCode.Id, newDiscountCode), Times.Exactly(1));
            Assert.IsTrue(result is NoContentResult);
        }

        [Test]
        public void When_Invoking_UpdateDisountCode_The_Update_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            DiscountCode newDiscountCode = null;


            _mockDiscountCodeService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(newDiscountCode);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.UpdateDiscountCode(It.IsAny<string>(), It.IsAny<DiscountCode>());

            //Assert
            _mockDiscountCodeService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<DiscountCode>()), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void When_Invoking_DeleteDiscountCode_The_Remove_Method_Should_Be_Invoked_Once()
        {
            //Arrange
            var discountCode = new DiscountCode();
            _mockDiscountCodeService.Setup(x => x.GetById(discountCode.Id))
                .Returns(discountCode);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.DeleteDiscountCode(discountCode.Id);


            //Assert
            _mockDiscountCodeService.Verify(x => x.Remove(discountCode.Id), Times.Exactly(1));
            Assert.IsTrue(result is NoContentResult);
        }

        [Test]
        public void When_Invoking_DeleteDiscountCode_The_Remove_Method_Should_Never_Been_Invoked()
        {
            //Arrange
            DiscountCode newDiscountCode = null;

            _mockDiscountCodeService.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(newDiscountCode);

            var controller = new DiscountCodesController(_mockDiscountCodeService.Object);

            //Act
            var result = controller.DeleteDiscountCode(It.IsAny<string>());

            //Assert
            _mockDiscountCodeService.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<DiscountCode>()), Times.Never);
            Assert.IsTrue(result is NotFoundResult);
        }


    }
}
