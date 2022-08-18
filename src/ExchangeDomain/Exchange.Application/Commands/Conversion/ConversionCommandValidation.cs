using FluentValidation;

namespace Exchange.Application.Commands.Conversion
{
    public class ConversionCommandValidation : AbstractValidator<ConversionCommand>
    {
        public ConversionCommandValidation()
        {
            RuleFor(x => x.amount).NotNull().NotEqual(0).WithMessage("amount not allow 0.");
            RuleFor(x => x.symbol).NotNull().Must(x => x == null || x.Length == 3).WithMessage("symbol should contain three characters");

        }
    }
}
