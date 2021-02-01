namespace WEBApi.Controllers
{
    public class DrinksFromOrderByName
    {
        public string OrderId { get; set; }
        public string DrinkName { get; set; }
        public int NumbersOfDrinks { get; set; }

        public DrinksFromOrderByName()
        {

        }

        public DrinksFromOrderByName(string orderId, string drinkName, int numbersOfDrinks)
        {
            OrderId = orderId;
            DrinkName = drinkName;
            NumbersOfDrinks = numbersOfDrinks;
        }
    }
}