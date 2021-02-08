using System.Collections.Generic;
using System.Linq;
using System.IO;
using WEBApi.Models;

namespace WEBApi
{
    public class ReadDrinksFromCSV
    {
        public List<Drink> drinks;

        public ReadDrinksFromCSV(string path)
        {
            drinks = ProcessFile(path);
        }

        public List<Drink> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                        .Skip(1)
                        .Where(line => line.Length > 1)
                        .Select(ParseFromCsv)
                        .ToList();
        }

        public Drink ParseFromCsv(string line)
        {
            var columns = line.Split(',');

            return new Drink
            {
                DrinkName = columns[1],
                AviableNumbersOfDrink = int.Parse(columns[2]),
                DrinkPrice = double.Parse(columns[3])
            };
        }
    }
}
