using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGrade
{
    public class Statistics
    { 

        public Statistics()
        {
            Low = double.MaxValue;
            High = double.MinValue; 
            Count = 0;
            Sum = 0.0;
        }

        public void Add(double number)
        {
            Sum += number;
            Count ++;
            Low = Math.Min(Low, number);
            High = Math.Max(High, number);
        }

        public double Average
        {
            get
            {
                return Sum/Count;
            }
        }
        public double High;
        public double Low;
        public double Sum;
        public int Count;
        
    }
}