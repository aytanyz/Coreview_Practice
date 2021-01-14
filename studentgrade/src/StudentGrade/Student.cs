 using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGrade
{
    public abstract class Student : NamedObject, IStudent
    {        
        public Student(string name) : base(name)
        {
            Name = name;
        }

        public abstract void AddGrade(string courseName, double grade);
        public abstract Statistics GetStatistics();
        public void PrintGrades(Statistics result)
        {
            Console.WriteLine("High: " + result.High);
            Console.WriteLine("Low: " + result.Low);
            Console.WriteLine("Average: " + result.Average);
        }
    }

    public interface IStudent
    {
        void AddGrade(string name, double grade);
        Statistics GetStatistics();
        void PrintGrades(Statistics result);
        String Name{ get; set; }
    } 
}
    