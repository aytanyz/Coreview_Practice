using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WEBApi.Models;

namespace WEBApi
{
    public class ReadDiscountCodesFromCSV
    {
        public List<DiscountCode> discountCodes;

        public ReadDiscountCodesFromCSV()
        {
            discountCodes = ProcessFile("Files/DiscountCodes.csv");
            /*foreach (var item in discountCodes)
                Console.WriteLine(item.Code + " " + item.Id);*/
        }

        public List<DiscountCode> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                        .Skip(1)
                        .Where(line => line.Length > 1)
                        .Select(ParseFromCsv)
                        .ToList();
        }

        public DiscountCode ParseFromCsv(string line)
        {
            var columns = line.Split(',');

            return new DiscountCode
            {
                Code = columns[1],
                DiscountPercentage = int.Parse(columns[2])
            };
        }

    }
}
