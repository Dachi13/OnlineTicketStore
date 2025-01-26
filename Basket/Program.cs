var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.StoreBasketRoute();
app.Run();