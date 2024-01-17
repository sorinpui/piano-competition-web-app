using CompetitionApi.Application.Factories;
using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Services;
using CompetitionApi.Application.Validators;
using CompetitionApi.DataAccess;
using CompetitionApi.DataAccess.Repositories;
using CompetitionApi.Domain.Interfaces;
using CompetitionApi.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db context and repositories
string? connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<CompetitionDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRenditionRepository, RenditionRepository>();
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Validators
builder.Services.AddTransient<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddTransient<IValidator<CreateScoreRequest>,  CreateScoreRequestValidator>();
builder.Services.AddTransient<IValidator<PostCommentRequest>, PostCommentRequestValidator>();
builder.Services.AddSingleton<IValidatorsFactory, ValidatorsFactory>();

// Business logic services
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRenditionService, RenditionService>();
builder.Services.AddScoped<IStorageService,  StorageService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// Authentication
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

builder.Services.AddHttpContextAccessor();

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
