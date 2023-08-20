using Game.Forum.Application.Models.DTOs.Vote;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface IVoteService
    {
        //Ekle
        Task AddVote(AddVoteDto addVote);
    }
}
