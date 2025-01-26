namespace Basket.Basket.StoreBasket;

public class StoreToBasketCommandValidator : AbstractValidator<StoreToBasketCommand>
{
    public StoreToBasketCommandValidator()
    {
        RuleFor(basket => basket.TicketId).NotEmpty().WithMessage("TicketId is required");
        RuleFor(basket => basket.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}