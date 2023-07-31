using Accessories.BingoCardCreation;
using Accessories.TombolaCreation;
using BenchmarkDotNet.Toolchains.Results;
using BingoHall;
using BingoHall.Dapper;
using BingoHall.Tombolas.Services;
using BingoHall.Users.Mappings;
using BingoHall.Users.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PasswordsAndEncryption.Passwords;
using System.Text;
using BingoHall.Users.Services;
using BingoHall.Authorization.JwtTokens;
using BingoHall.Authorization.JwtTokens.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddTransient<ICardDataFactory, CardDataFactory>();
builder.Services.AddTransient<IBingoCardService, BingoCardService>();
builder.Services.AddTransient<BingoUserMapper>();
builder.Services.AddSingleton<ITombolaService, TombolaService>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IUserDao, UserDao>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddTransient<IJwtTokenGeneratorConfiguration, JwtTokenGeneratorConfigurationDI>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
