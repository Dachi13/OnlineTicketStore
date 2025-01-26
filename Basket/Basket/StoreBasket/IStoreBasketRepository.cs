namespace Basket.Basket.StoreBasket;

public interface IStoreBasketRepository
{
    Task<bool> StoreToBasket();
}