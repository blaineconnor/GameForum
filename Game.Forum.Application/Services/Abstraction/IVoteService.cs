using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.DTOs.Vote;
using Game.Forum.Application.Models.RequestModels.Votes;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface IVoteService
    {
        //Ekle
        Task AddVote(AddVoteDto addVote);
        Task DeleteVote(DeleteVoteVM deleteVoteVM);
    }
}
