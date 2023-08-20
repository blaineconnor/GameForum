
using Game.Forum.Domain.Entities;

namespace Game.Forum.Domain.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<PaginationResponse<GetAllQuestions>> GetNewestQuestions(Pagination pagination);
        Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingVote(Pagination pagination);
        Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingAnswer(Pagination pagination);

        Task<QuestionDetailResponse> GetQuestionsWithDetail(int id);
    }
}
