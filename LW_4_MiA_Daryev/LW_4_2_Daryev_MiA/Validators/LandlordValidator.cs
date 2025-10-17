using FluentValidation;
using LW_4_2_Daryev_MiA.Models;

namespace LW_4_2_Daryev_MiA.Validators
{
    public class LandlordValidator : AbstractValidator<LandlordClass>
    {
        public LandlordValidator()
        {
            RuleFor(landlord => landlord.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(landlord => landlord.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(landlord => landlord.LandlordEmail)
                .NotEmpty().WithMessage("LandlordEmail is required.")
                .EmailAddress().WithMessage("Incorrect email")
                .WithMessage("LandlordEmail must be a valid email address.");
        }
    }
}
