using CompetitionWebApi.Application.Services;
using CompetitionWebApi.Application.Validators;
using CompetitionWebApi.DataAccess;
using CompetitionWebApi.Domain.Interfaces;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CompetitionWebApi.Application.Factories;
using CompetitionWebApi.DataAccess.Repositories;
using System.Text.Json;
using CompetitionWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context and repositories
string? connectionString = builder.Configuration.GetConnectionString("CompetitionDb");
builder.Services.AddDbContext<CompetitionDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Request validators
builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddTransient<IValidator<PerformanceRequest>, PerformanceRequestValidator>();
builder.Services.AddTransient<IValidator<ScoreRequest>, ScoreValidator>();
builder.Services.AddTransient<IValidator<CreateJudgeRequest>, CreateJudgeRequestValidator>();
builder.Services.AddSingleton<IValidatorsFactory, ValidatorsFactory>();

// Validation services
builder.Services.AddScoped<IValidationService, ValidationService>();

// Application services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IFilesService, FileService>();
builder.Services.AddScoped<IScoreService, ScoreService>();

// Authentication services
builder.Services.AddScoped<IJwtService, JwtService>();

string? secretKey = builder.Configuration["JwtSettings:SecretKey"];

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
