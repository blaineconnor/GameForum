using FluentValidation;
using FluentValidation.AspNetCore;
using Game.Forum.UI.Validators.Accounts;

namespace Game.Forum.UI.Configurations
{
    public static class FluentValidationInjection
    {
        public static IServiceCollection AddFluentValidationService(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining(typeof(LoginValidator));

            return services;
        }
    }
}
