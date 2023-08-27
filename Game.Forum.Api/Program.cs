using FluentValidation;
using Game.Forum.Api.Filters;
using Game.Forum.Application.AutoMappings;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Services.Implementation;
using Game.Forum.Application.Validators.Categories;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Implementation;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.Services.Abstraction;
using Game.Forum.Domain.Services.Implementation;
using Game.Forum.Domain.UnitofWork;
using Game.Forum.Infrastructure.Steam.Services.Abstraction;
using Game.Forum.Infrastructure.Steam.Services.Implementation;
using Game.Forum.Persistence.Context;
using Game.Forum.Persistence.Repositories;
using Game.Forum.Persistence.UnitofWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Logging
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

Log.Logger.Information("Program Started...");

// Add services to the container.

//ActionFilter registiration
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ExceptionHandlerFilter());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameGourmetForum", Version = "v1", Description = "requires JwtTokenWithIdentity" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<GameForumContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GameForum"));
});

//Repos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUnitofWork, UnitWork>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();


//Servisler
builder.Services.AddScoped<ILoggedUserService, LoggedUserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<ISteamService, SteamService>();
builder.Services.AddSingleton<ICultureService, CultureService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


//Redis
builder.Services.AddScoped<IRedisCache, RedisCacheManager>();
builder.Services.AddScoped<IFavoriteCache, FavoriteCache>();
builder.Services.AddScoped<IQuestionDetailCache, QuestionDetailCache>();
builder.Services.AddScoped<IVoteCache, VoteCache>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(DomainToDto), typeof(ViewModelToDomain));

//FluentValidation, istekte g�nderilen modele ait property, istenen format� destekliyor mu?
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryValidator));



// JWT kimlik do�rulama servisini ekleme
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Token� olu�turan taraf�n adresi
            ValidAudience = builder.Configuration["Jwt:Audiance"], // Token�n kullan�laca�� hedef adres
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])) // Gizli anahtar
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();

