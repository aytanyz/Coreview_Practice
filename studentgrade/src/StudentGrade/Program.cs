using System;
using System.Collections.Generic;

namespace StudentGrade
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student_1 = new Student("Aytan");
            student_1.AddGrade("Math", 26);
            student_1.AddGrade("Info", 24);
            student_1.AddGrade("Chemistry", 30);
            student_1.PrintGrades();
            student_1.GetStatistics();
        }
    }
}
