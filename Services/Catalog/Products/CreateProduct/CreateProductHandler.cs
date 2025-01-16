namespace Catalog.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    int CategoryId,
    string Description,
    string ImageFile,
    decimal Price)
    : ICommand<Result<CreateProductResult>>;

public record CreateProductResult(long Id);

internal class CreateProductCommandHandler(IProductRepository repository)
    : ICommandHandler<CreateProductCommand, Result<CreateProductResult>>
{
    public async Task<Result<CreateProductResult>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = ProductValidations.ValidateCreateProductRequest(command);

        if (!validationResult.IsValid)
            return new Error("", validationResult.Errors[0].ErrorMessage, ErrorType.Validation);

        var result = await repository.AddProductAsync(command);

        return result.IsSuccess
            ? new CreateProductResult(result.Value)
            : result.Error;
    }
}