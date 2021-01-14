using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGrade
{
    public class InMemoryStudent : Student
    {   
        public InMemoryStudent(string name) : base(name)
        {
            Name = name;
            courseGrade = new Dictionary<string, double>();
        }

        public override void AddGrade(string courseName, double grade)
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

        public override Statistics GetStatistics()
        {
            Statistics result = new Statistics();
            
            foreach( var grade in courseGrade.Values)
            {
                result.Add(grade);
            }

            return result;
        }

        private Dictionary<string, double> courseGrade;
        
        //public string Name { get; set; }

        // Below after constructor you cannot modify Name
            // public string Name { get; private set;}
        // Or using readonly. Only constractor or variable initializer can change value
            // readonly string Name = "Science";
        // const cannot be modified even from constractor  
            // const string MY_CONST = "sjhdj";  

        /*
        // long way of writing get/set
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    name = value;
                }
            }
        } 
        private string name;        
        */
        }
}
