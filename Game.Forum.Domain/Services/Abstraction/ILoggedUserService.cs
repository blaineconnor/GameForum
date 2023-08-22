
using Game.Forum.Domain.Entities;

namespace Game.Forum.Domain.Services.Abstraction
{
    public interface ILoggedUserService
    {
        int? UserId { get; }
        Roles? Role { get; }
        string Username { get; }
        string Email { get; }

        
    }
}
