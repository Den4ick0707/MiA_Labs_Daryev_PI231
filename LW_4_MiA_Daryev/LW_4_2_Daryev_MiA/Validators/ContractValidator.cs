using FluentValidation;
using LW_4_2_Daryev_MiA.Models;

namespace LW_4_2_Daryev_MiA.Validators
{
    public class ContractValidator : AbstractValidator<ContractClass>
    {
        public ContractValidator() {
            RuleFor(contract => contract.ContractId)
                    .Null().Empty().WithMessage("Contract Id is a required");
            RuleFor(contract => contract.ContractDay)
                .Empty().Null().WithMessage("Contract day is a required");
        }
    }
}
