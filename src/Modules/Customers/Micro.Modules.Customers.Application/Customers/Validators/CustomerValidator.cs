using FluentValidation;

namespace Micro.Modules.Customers.Application.Customers.Validators;

internal class CustomerValidator : AbstractValidator<AddCustomer>
{
    public CustomerValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
            .WithErrorCode("name_required")
            .WithMessage("Product name cannot be empty");
       
    }
}
