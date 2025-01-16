using System.Data;
using Catalog.Products.CreateProduct;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("Database");
    return new NpgsqlConnection(connectionString);
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<DapperContext>(args => new DapperContext(args.GetRequiredService<IConfiguration>()));
// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    // config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    // config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
// builder.Services.AddValidatorsFromAssembly(assembly);

// builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// builder.Services.AddHealthChecks()
//     .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler(options => { });

// app.UseHealthChecks("/health",
//     new HealthCheckOptions
//     {
//         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//     });

// var createProductEndpoint = new CreateProductEndpoint();
// createProductEndpoint.AddRoutes(app);

app.AddRoutes();

app.Run();