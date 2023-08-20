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
using Game.Forum.Infrastructure.Steam.Services.Abstraction;
using Game.Forum.Infrastructure.Steam.Services.Implementation;
using Game.Forum.Persistence.Context;
using Game.Forum.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Logging
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();



Log.Logger.Information("Program Started...");

// Add services to the container.

//ActionFilter registiration
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ExceptionHandlerFilter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();



builder.Services.AddDbContext<GameForumContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GameForum"));
});

//Repos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();


//Servisler
builder.Services.AddScoped<IUserService, UserService>();
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
builder.Services.AddAutoMapper(typeof(DomainToDto), typeof(ViewModelToDomain));

builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryValidator));


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

app.UseStaticFiles();

app.Run();

