namespace Catalog.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    int CategoryId,
    string Description,
    string ImageFile,
    decimal Price);

public record CreateProductResponse(long Id);

public static class CreateProductEndpoint
{
    public static void AddRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();

                    var result = await sender.Send(command);

                    return result.Match(
                        _ => Results.Created($"/products/{result.Value.Id}", result.Value),
                        error => error switch
                        {
                            { ErrorType: ErrorType.DatabaseError or ErrorType.InternalServerError } => Results.Conflict(
                                new { message = error.Message }),
                            { ErrorType: ErrorType.Validation } => Results.BadRequest(new { message = error.Message }),
                            _ => Results.Problem(error.Message)
                        });
                })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}