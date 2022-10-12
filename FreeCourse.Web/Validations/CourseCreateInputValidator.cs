using FluentValidation;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Validations
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name must not be empty");
            RuleFor(i => i.Description).NotEmpty().WithMessage("description can not be empty");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1,int.MaxValue).WithMessage("duration can not be empty");

        }
    }
}
