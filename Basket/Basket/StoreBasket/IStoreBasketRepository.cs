namespace Basket.Basket.StoreBasket;

public interface IStoreBasketRepository
{
    Task<Result<long?>> StoreToBasket(EventsBasket basket);
}