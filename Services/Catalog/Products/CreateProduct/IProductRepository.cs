namespace Catalog.Products.CreateProduct;

public interface IProductRepository
{
    Task<Result<long>> AddProductAsync(CreateProductCommand productCommand);
}