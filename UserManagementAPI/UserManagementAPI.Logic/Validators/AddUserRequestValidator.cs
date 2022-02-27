using FluentValidation;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Age)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
