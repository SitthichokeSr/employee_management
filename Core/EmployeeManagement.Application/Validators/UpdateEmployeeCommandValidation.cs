using EmployeeManagement.Application.Features;
using FluentValidation;

namespace EmployeeManagement.Application.Validators
{
    internal class UpdateEmployeeCommandValidation : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidation()
        {
            RuleFor(x => x.id)
                .NotNull();

            RuleFor(x => x.first_name)
                .NotEmpty()
                .MaximumLength(512);

            RuleFor(x => x.last_name)
                .NotEmpty()
                .MaximumLength(512);

            RuleFor(x => x.designation)
                .NotEmpty();

            RuleFor(x => x.hire_date)
                .NotEmpty()
                .Must(h => h <= DateTimeOffset.Now)
                .WithMessage(x => $"Hire date ({x.hire_date}) cannot be in the future. Current time is ({DateTimeOffset.Now}).");

            RuleFor(x => x.salary)
                .PrecisionScale(20, 6, false)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Salary must be a non-negative value.");

            RuleFor(x => x.dept_no)
                .NotEmpty();
        }
    }
}
