using DotNetCore_CRUD_API.Models;
using FluentValidation;

namespace DotNetCore_CRUD_API.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .Length(2, 50)
                .WithMessage("Name must be between 2 and 50 characters long");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        }
    }
}
