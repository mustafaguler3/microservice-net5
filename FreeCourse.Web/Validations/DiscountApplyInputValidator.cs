using FluentValidation;
using FreeCourse.Web.Models.Discount;

namespace FreeCourse.Web.Validations
{
    public class DiscountApplyInputValidator :AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(i => i.Code).NotEmpty().WithMessage("this field can not be empty");
        }
    }
}
