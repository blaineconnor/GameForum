using Game.Forum.Application.Models.DTOs.Accounts;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Accounts;
using Game.Forum.Application.Wrapper;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface IAccountService
    {
        Task<Result<bool>> Register(RegisterVM createUserVM);

        Task<Result<bool>> Login(LoginVM loginVM);

        Task<Result<bool>> UpdateUser(UpdateUserVM updateUserVM);
        Task DeleteUser(DeleteDto deleteDto);
    }
}
