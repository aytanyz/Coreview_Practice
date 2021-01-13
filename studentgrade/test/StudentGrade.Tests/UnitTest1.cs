using System;
using Xunit;

namespace StudentGrade.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var student = new Student("");
            student.AddGrade("Math", 20);
            student.AddGrade("Info", 14);
            student.AddGrade("Chemistry", 19);
            
            //act
            var result = student.GetStatistics();

            //assert
            Assert.Equal(14, result.Low, 1);
            Assert.Equal(20, result.High, 1);
            Assert.Equal(17.7, result.Average, 1);
        }
    }
}
