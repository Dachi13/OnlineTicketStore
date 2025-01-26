namespace Basket.Basket.StoreBasket;

public record StoreToBasketCommand(long TicketId, int Amount) : ICommand<StoreToBasketResult>;

public record StoreToBasketResult(bool StoredSuccessfully);

public class StoreBasketHandler : ICommandHandler<StoreToBasketCommand, StoreToBasketResult>
{
    public Task<Result<StoreToBasketResult>> Handle(StoreToBasketCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}