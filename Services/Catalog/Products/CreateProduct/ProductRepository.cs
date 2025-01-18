namespace Catalog.Products.CreateProduct;

public class ProductRepository(DapperContext context) : IProductRepository
{
    public async Task<Result<long>> AddProductAsync(CreateProductCommand productCommand)
    {
        try
        {
            var imageBytes = Convert.FromBase64String(productCommand.ImageFile);

            await using var connection = await context.CreateConnectionAsync();

            var parameters = new DynamicParameters();

            parameters.Add("p_name", productCommand.Name);
            parameters.Add("p_categoryid", (short)productCommand.CategoryId);
            parameters.Add("p_description", productCommand.Description);
            parameters.Add("p_imagefile", imageBytes);
            parameters.Add("p_price", productCommand.Price);
            parameters.Add("productid", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "public.spaddproduct",
                parameters,
                commandType: CommandType.StoredProcedure);

            var productId = parameters.Get<long>("productid");

            return productId;
        }
        catch (PostgresException exception)
        {
            return new Error("Database_Exception", exception.Message, ErrorType.DatabaseError);
        }
        catch (Exception exception)
        {
            return new Error("Internal_Error", exception.Message, ErrorType.InternalServerError);
        }
    }
}