using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Answers;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface IAnswerService
    {
        //Ekle, Sil
        Task AddAnswerAsync(AddAnswerVM addAnswerVM);
        Task DeleteAnswerAsync(DeleteDto deleteDto);

    }
}
