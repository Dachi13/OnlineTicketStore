namespace Catalog.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    int CategoryId,
    string Description,
    string ImageFile,
    decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(long Id);

internal class CreateProductCommandHandler(IProductRepository repository)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<Result<CreateProductResult>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await repository.AddProductAsync(command);

        return result.IsSuccess
            ? new CreateProductResult(result.Value)
            : result.Error;
    }
}