using FluentValidation;

namespace WEBApi.Models.Validators
{
    public class IdValidator : AbstractValidator<string>
    {
        public IdValidator()
        {
            RuleFor(id => id).NotNull(); 
            //RuleFor(id => id).Length(0, 3);
        }
    }
}
