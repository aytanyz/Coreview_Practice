using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class DiscountCode
    {
        public string Code { get; set; }
        public int Percentage { get; set; }

        public DiscountCode() { }

        public DiscountCode(string code, int percentage)
        {
            Code = code;
            Percentage = percentage;
        }
    }
}
