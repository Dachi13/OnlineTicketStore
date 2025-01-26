namespace Basket.Basket.StoreBasket;

public record StoreToBasketRequest(long TicketId, int Amount);

public record StoreToBasketResponse(bool StoredSuccessfully);

public static class StoreBasketEndpoint
{
    public static void StoreBasketRoute(this IEndpointRouteBuilder app)
    {
        app.MapPost("/AddBasket", async (StoreToBasketRequest request, ISender sender) =>
            {
                var storeBasketCommand = request.Adapt<StoreToBasketCommand>();

                var result = await sender.Send(storeBasketCommand);

                return result.Match(
                    _ => Results.Created($"/products/{result.Value.StoredSuccessfully}", result.Value),
                    error => error switch
                    {
                        { ErrorType: ErrorType.InternalServerError } => Results.InternalServerError(new
                            { message = error.Message }),
                        _ => Results.Conflict(error.Message)
                    });
            })
            .WithName("Store to Basket")
            .Produces<StoreToBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store to Basket")
            .WithDescription("Store to Basket");
        ;
    }
}