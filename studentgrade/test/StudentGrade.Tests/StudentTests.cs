using System;
using Xunit;

namespace StudentGrade.Tests
{
    public class StudentTests
    {
        [Fact]
        public void TestGetStatistics()
        {
            //arrange
            var student = new InMemoryStudent("");

            try
            {                    
                student.AddGrade("Math", 20);
                student.AddGrade("Info", 14);
                student.AddGrade("Chemistry", 19);
            }
            /*
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            */
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            // it happens even if there appear any exception 
            finally
            {
                Console.WriteLine("****");
            }
            
            //act
            var result = student.GetStatistics();

            //assert
            Assert.Equal(14, result.Low, 1);
            Assert.Equal(20, result.High, 1);
            Assert.Equal(17.7, result.Average, 1);
        }
    }
}
