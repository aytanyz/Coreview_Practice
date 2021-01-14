using System;
using System.Collections.Generic;

namespace StudentGrade
{
    class Program
    {
        static void Main(string[] args)
        {
            IStudent student_1 = new InMemoryStudent("Aytan");
            var result_1 = new Statistics();
            student_1.AddGrade("Math", 26);
            student_1.AddGrade("Info", 24);
            student_1.AddGrade("Chemistry", 36);
            result_1 = student_1.GetStatistics();
            student_1.PrintGrades(result_1);

            IStudent student_2 = new InFileStudent("Tabriz");
            var result_2 = new Statistics();
            student_2.AddGrade("Math", 30);
            student_2.AddGrade("Info", 20);
            result_2 = student_2.GetStatistics();
            student_2.PrintGrades(result_2);
        }
    }
}
