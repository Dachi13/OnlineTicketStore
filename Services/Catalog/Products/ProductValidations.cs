using FluentValidation.Results;
using Catalog.Products.CreateProduct;

namespace Catalog.Products;

public static class ProductValidations
{
    public static ValidationResult ValidateCreateProductRequest(CreateProductCommand request)
    {
        var validator = new CreateProductRequestValidator();
        var result = validator.Validate(request);

        return result;
    }
}