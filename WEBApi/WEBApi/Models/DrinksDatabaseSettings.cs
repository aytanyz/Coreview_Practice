using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Models
{
    public class DrinksDatabaseSettings : IDrinksDatabaseSettings
    {
        public string DrinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDrinksDatabaseSettings
    {
        string DrinksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
