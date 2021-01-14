using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StudentGrade
{
    public class InFileStudent : Student, IStudent
    {
        public InFileStudent(string name) : base(name)
        {
            Name = name;
        }

        public override void AddGrade(string courseName, double grade)
        {
            using(var wr = File.AppendText($"{Name}.txt"))
            {
                wr.WriteLine(grade);
            }            
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using(var rd = File.OpenText($"{Name}.txt"))
            {
                var line = rd.ReadLine();
                while (line != null)
                {
                    //Console.WriteLine(line);
                    var number = double.Parse(line);
                    result.Add(number);
                    line = rd.ReadLine();
                }
            }   
            return result;
        }
    }
}