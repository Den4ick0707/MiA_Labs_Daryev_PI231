using FluentValidation;
using LW_4_2_Daryev_MiA.Models;

namespace LW_4_2_Daryev_MiA.Validators
{
    public class DeviceValidator : AbstractValidator<Device>
    {
        public DeviceValidator()
        {
            RuleFor(device => device.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(device => device.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
            RuleFor(device => device.LandlordID)
                .NotEmpty().WithMessage("LandlordID is required.");
            RuleFor(device => device.Type)
                .IsInEnum().WithMessage("Type must be a valid DeviceType.");
        }
    }
}
