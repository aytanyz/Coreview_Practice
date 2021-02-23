using FluentValidation;

namespace WEBApi.Models
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).Length(24);

            RuleFor(x => x.OrderedDrinks.Count).GreaterThanOrEqualTo(1);
        }
    }
}
