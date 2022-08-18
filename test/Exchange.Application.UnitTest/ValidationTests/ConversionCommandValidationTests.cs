using Exchange.Application.Commands.Conversion;
using FluentValidation.TestHelper;

namespace Exchange.Application.UnitTest.ValidationTests
{
    public class ConversionCommandValidationTests
    {
        private ConversionCommandValidation validator;

        public ConversionCommandValidationTests()
        {
            validator = new ConversionCommandValidation();
        }

        [Fact]
        public async void Validator_HaveErrorWhenAmountIsNotValid_ReturnError()
        {
            var model = new ConversionCommand("CAD",0);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.amount);
        }

        [Fact]
        public async void Validator_HaveErrorWhenSymbolIsNotThreeChar_ReturnError()
        {
            var model = new ConversionCommand("CADDD", 10);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.symbol);
        }
    }
}
