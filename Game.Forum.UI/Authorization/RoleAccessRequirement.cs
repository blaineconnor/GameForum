using Game.Forum.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Game.Forum.UI.Authorization
{
    public class RoleAccessRequirement : IAuthorizationRequirement
    {
        public Roles[] Roles { get; set; }

        public RoleAccessRequirement(params Roles[] roles)
        {
            Roles = roles;
        }
    }
}
