namespace Catalog.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductRequestValidator()
    {
        RuleFor(product => product.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");

        RuleFor(product => product.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .InclusiveBetween(1, 32_767).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");

        RuleFor(product => product.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 1000).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");

        RuleFor(product => product.ImageFile)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty");

        RuleFor(product => product.Price)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .GreaterThan(0).WithMessage("Price cannot be less than 1");
    }
}