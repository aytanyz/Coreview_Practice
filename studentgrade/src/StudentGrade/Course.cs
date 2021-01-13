using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGrade
{
    class Course
    {
        public void AddCourse(string courseName, string profName, string profSurname)
        {
            this.courseName = courseName;
            this.profName = profName;
            this.profSurname = profSurname;
        }

        private string courseName;
        private string profName;
        private string profSurname;
    }
}
