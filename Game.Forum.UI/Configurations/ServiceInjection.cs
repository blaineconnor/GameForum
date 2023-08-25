using Game.Forum.UI.Services.Abstraction;
using Game.Forum.UI.Services.Implementation;

namespace Game.Forum.UI.Configurations
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IRestService, RestService>();

            return services;
        }
    }
}
