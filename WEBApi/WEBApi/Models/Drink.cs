namespace WEBApi.Models
{
    public class Drink : EntityBase
    {
        public string DrinkName { get; set; }
        public int AviableNumbersOfDrink { get; set; }
        public double DrinkPrice { get; set; }

        public Drink()
        {

        }
        public Drink(string drinkName, int aviableNumbersOfDrink, double price)
        {
            DrinkName = drinkName;
            AviableNumbersOfDrink = aviableNumbersOfDrink;
            DrinkPrice = price;
        }
    }
}
