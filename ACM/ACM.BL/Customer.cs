using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    class Customer
    {
        private string _lastname;
        public int EmailAddress { get; set; }
        public int CustomerId { get; private set; }
        public string FirstName { get; set; }
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
    }
}
