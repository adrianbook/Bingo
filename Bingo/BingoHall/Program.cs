using Accessories.BingoCardCreation;
using Accessories.TombolaCreation;
using BenchmarkDotNet.Toolchains.Results;
using BingoHall;
using BingoHall.Dapper;
using BingoHall.Tombolas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICardDataFactory, CardDataFactory>();
builder.Services.AddTransient<IBingoCardService, BingoCardService>();
builder.Services.AddSingleton<ITombolaService, TombolaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
