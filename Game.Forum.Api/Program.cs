using FluentValidation;
using FluentValidation.AspNetCore;
using Game.Forum.Api.Middleware;
using Game.Forum.Application.AutoMappings;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Services.Implementation;
using Game.Forum.Application.Validators;
using Game.Forum.Application.Validators.Categories;
using Game.Forum.Application.Validators.Users;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Implementation;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.UnitofWork;
using Game.Forum.Infrastructure.Steam.Services.Abstraction;
using Game.Forum.Infrastructure.Steam.Services.Implementation;
using Game.Forum.Persistence.Context;
using Game.Forum.Persistence.Repositories;
using Game.Forum.Persistence.UnitofWork;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging();
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidation(u => u.RegisterValidatorsFromAssemblyContaining<RegisterValidator>());
builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "127.0.0.1:6379";
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<GameForumContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Repository Registiraction
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddTransient<IVoteRepository, VoteRepository>();
//UnitOfWork Registiration
builder.Services.AddTransient<IUnitofWork, UnitWork>();

//Business Service Registiration
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IAnswerService, AnswerService>();
builder.Services.AddTransient<IVoteService, VoteService>();

//Infrastructure Service Registiration
builder.Services.AddSingleton<ICultureService, CultureService>();
builder.Services.AddScoped<ISteamService, SteamService>();

//Cache
builder.Services.AddTransient<IRedisCache, RedisCacheManager>();
builder.Services.AddTransient<IFavoriteCache, FavoriteCache>();
builder.Services.AddTransient<IQuestionDetailCache, QuestionDetailCache>();
builder.Services.AddTransient<IVoteCache, VoteCache>();

//Automapper
builder.Services.AddAutoMapper(typeof(DomainToDto), typeof(ViewModelToDomain));

//FluentValidation Ýstekte gönderilen modele ait property'lerin istenen formatý destekleyip desteklemediðini anlamamýzý saðlar.
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryValidator));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestResponseLogMiddleware>();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        //.WriteTo.Debug()
        //.WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}