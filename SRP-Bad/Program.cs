using SOLID.BLogic;
using SOLID.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<ILogger, ILogger>();
//builder.Services.AddTransient<IConfiguration, Configuration>();
builder.Services.AddTransient<IUserBLogic, UserBLogic>();
builder.Services.AddTransient<IUserData, UserData>();
builder.Services.AddTransient<IProductoBLogic, ProductoBLogic>();
builder.Services.AddTransient<IProductoData, ProductoData>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthorization();

app.MapControllers();

app.Run();
