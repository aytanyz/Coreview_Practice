using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class CustomerRepository
    {
        public Customer Retrieve(int customeId)
        {
            Customer customer = new Customer(customeId);

            if(customeId == 1)
            {
                customer.EmailAddress = "fb@gmail.com";
                customer.FirstName = "Frodo";
                customer.LastName = "Baggins";
            }

            return customer;
        }

        public bool Save()
        {
            return true;
        }
    }
}
