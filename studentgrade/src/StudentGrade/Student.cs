using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGrade
{
    public class Student
    {
        public Student(string name)
        {
            Name = name;
        }

        public void AddGrade(string courseName, double grade)
        {
            if(grade>=0 && grade<=100)
            {
                courseGrade.Add(courseName, grade);
            }
            else
            {
                throw new ArgumentException($"Invalid grade");
            }
        }

        public void PrintGrades()
        {
            foreach (var course in courseGrade)
            {
                Console.WriteLine(course.Key + " : " + course.Value);
            }
        }

        public Statistics GetStatistics()
        {
            Statistics result = new Statistics();
            result.Low = double.MaxValue;
            result.High = double.MinValue; 
            result.Average = 0.0; 

            foreach( var grade in courseGrade.Values)
            {
                result.Low = Math.Min(result.Low, grade);
                result.High = Math.Max(result.High, grade);
                result.Average += grade; 
            }
            result.Average /= courseGrade.Count;

            Console.WriteLine(result.Low);
            Console.WriteLine(result.High);
            Console.WriteLine(result.Average);

            return result;
        }

        public string Name;
        private Dictionary<string, double> courseGrade = new Dictionary<string, double>();
    }
}
