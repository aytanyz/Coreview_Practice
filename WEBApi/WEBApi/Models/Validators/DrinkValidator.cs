using FluentValidation;

namespace WEBApi.Models.Validators
{
    public class DrinkValidator : AbstractValidator<Drink>
    {
        public DrinkValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("{PropertyName} is null.");
            
            RuleFor(x => x.DrinkName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("{PropertyName} is null.")
                .Length(2, 50)
                .WithMessage("{PropertyName} has invalid length.");

            RuleFor(x => x.DrinkPrice)
                .GreaterThan(0)
                .WithMessage("{PropertyName} is invalid.");

            RuleFor(x => x.AviableNumbersOfDrink)
                .GreaterThan(0)
                .WithMessage("{PropertyName} is invalid.");
        }
    }
}
