using System;
using Xunit;

namespace StudentGrade.Tests
{
    public class TypeTests
    {
        [Fact]
        public void CsharpCanPassByRef()
        {
            //arrange
            var student_1 = GetStudent("Aytan");
            GetNameSetNameRef(ref student_1, "New Name");
            
            //act

            //assert
            Assert.Equal(student_1.Name, "New Name");
        }

        private void GetNameSetNameRef( ref InMemoryStudent student, string name)
        {
            student = new InMemoryStudent(name);
        }

        /*---------------------------------------------------------------------*/[Fact]
        public void CsharpIsPassByValue()
        {
            //arrange
            var student_1 = GetStudent("Aytan");
            GetNameSetName(student_1, "New Name");
            
            //act

            //assert
            Assert.Equal(student_1.Name, "Aytan");
        }

        private void GetNameSetName(InMemoryStudent student, string name)
        {
            student = new InMemoryStudent(name);
        }

        /*---------------------------------------------------------------------*/
        [Fact]
        public void CanSetNameFromRef()
        {
            //arrange
            var student_1 = GetStudent("Aytan");
            SetName(student_1, "New Name");
            
            //act

            //assert
            Assert.Equal(student_1.Name, "New Name");
        }

        private void SetName(InMemoryStudent student, string name)
        {
            student.Name = name;
        }

        /*-------------------------------------------------------------------------*/
        [Fact]
        public void GetStudentReturnsDifferentObjects()
        {
            //arrange
            var student_1 = GetStudent("Aytan");
            var student_2 = GetStudent("Sevda");
            
            //act

            //assert
            Assert.Equal("Aytan", student_1.Name);
            Assert.Equal("Sevda", student_2.Name);
            // other way to do same testing
            Assert.NotSame(student_1, student_2);
        }

        [Fact]
        public void TwoVarsCanRefSameObject()
        {
            //arrange
            var student_1 = GetStudent("Aytan");
            var student_2 = student_1;
            
            //act

            //assert
            Assert.Same(student_1, student_2);
            // other way to do same testing
            Assert.True(Object.ReferenceEquals(student_1, student_2));
        }

        InMemoryStudent GetStudent(string name)
        {
            return new InMemoryStudent(name);
        }
    }
}
