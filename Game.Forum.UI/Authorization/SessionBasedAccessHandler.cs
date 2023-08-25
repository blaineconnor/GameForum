using Game.Forum.UI.Models.DTOs.Accounts;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Game.Forum.UI.Authorization
{
    public class SessionBasedAccessHandler : AuthorizationHandler<RoleAccessRequirement>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public SessionBasedAccessHandler(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAccessRequirement requirement)
        {
            var sessionKey = _configuration["Application:SessionKey"];

            if (_contextAccessor.HttpContext.Session?.GetString(sessionKey) is null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var userInfo = JsonConvert.DeserializeObject<TokenDto>(_contextAccessor.HttpContext.Session?.GetString(sessionKey));
            if (requirement.Roles.Contains(userInfo.Role))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
