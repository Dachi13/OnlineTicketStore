var builder = WebApplication.CreateBuilder(args);

// {
//     EventsBasket basket = new EventsBasket();
//     basket.Events =
//     [
//         new Event
//         {
//             TicketId = -1,
//             Amount = 1,
//             Price = (decimal)12.99
//         },
//         new Event
//         {
//             TicketId = -2,
//             Amount = 1,
//             Price = (decimal)13.99
//         }
//     ];
//     
//     var json = System.Text.Json.JsonSerializer.Serialize(basket, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
//     Console.WriteLine(json);
//     return;
// }

var connectionString = builder.Configuration.GetConnectionString("Database")!;

// Configure Database
builder.Services.AddScoped<DapperContext>(sp => new DapperContext(sp.GetRequiredService<IConfiguration>()));
builder.Services.AddSingleton<IDbConnection>(_ => new NpgsqlConnection(connectionString));
builder.Services.AddScoped<IStoreBasketRepository, StoreBasketRepository>();

// Add services to the container.
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().AddNpgSql(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(_ => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.AddBasketRoute();
app.Run();