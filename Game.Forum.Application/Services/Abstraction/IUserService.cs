using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Users;
using Game.Forum.Application.Wrapper;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface IUserService
    {
        int? UserId { get; }
        Roles? Role { get; }
        string Username { get; }
        string Email { get; }

        Task<Result<bool>> Register(RegisterVM createUserVM);

        Task<Result<User>> Login(LoginVM loginVM);

        Task<Result<bool>> UpdateUser(UpdateUserVM updateUserVM);
        Task DeleteUser(DeleteDto deleteDto);
    }
}
