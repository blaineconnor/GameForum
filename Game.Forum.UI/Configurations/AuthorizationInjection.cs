using Game.Forum.UI.Authorization;
using Game.Forum.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Game.Forum.UI.Configurations
{
    public static class AuthorizationInjection
    {
        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, SessionBasedAccessHandler>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy =>
                {
                    policy.AddRequirements(new RoleAccessRequirement(Roles.Admin));
                });

                opt.AddPolicy("User", policy =>
                {
                    policy.AddRequirements(new RoleAccessRequirement(Roles.User));
                });

                opt.AddPolicy("AdminOrUser", policy =>
                {
                    policy.AddRequirements(new RoleAccessRequirement(Roles.Admin, Roles.User));
                });

            });

            return services;
        }
    }
}
