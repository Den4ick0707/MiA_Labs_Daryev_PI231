using FluentValidation;
using LW_4_2_Daryev_MiA.Models;
namespace LW_4_2_Daryev_MiA.Validators
{
    public class UserValidator : AbstractValidator<UserClass>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Matches("^[\\w\\.\\-]+@[a-zA-Z0-9\\-]+\\.[a-zA-Z]{2,}$");
        }
    }
}
