using CompetitionWebApi.Application.Services;
using CompetitionWebApi.Application.Validators;
using CompetitionWebApi.DataAccess;
using CompetitionWebApi.Domain.Interfaces;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Persistance;
using CompetitionWebApi.Persistance.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using CompetitionWebApi.Application.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context and repositories
string? connectionString = builder.Configuration.GetConnectionString("CompetitionDb");
builder.Services.AddDbContext<CompetitionDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Request validators
builder.Services.AddTransient<IValidator<RegisterRequest>, ReigsterRequestValidator>();
builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();

builder.Services.AddSingleton<IValidatorsFactory, ValidatorsFactory>();

// Application services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

// Authentication services
builder.Services.AddScoped<IJwtService, JwtService>();

string? secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

if (secretKey is null)
{
    secretKey = Encoding.UTF8.GetString(RandomNumberGenerator.GetBytes(32));
    Environment.SetEnvironmentVariable("JWT_SECRET_KEY", secretKey);
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

app.Run();
