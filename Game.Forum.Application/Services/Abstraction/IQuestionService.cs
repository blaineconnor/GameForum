﻿using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface IQuestionService
    {
        #region Seç

        Task<PaginationResponse<GetAllQuestions>> GetNewestQuestions(Pagination pagination);
        Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingVote(Pagination pagination);
        Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingAnswer(Pagination pagination);
        Task<QuestionDetailResponse> GetQuestionsWithDetail(int id, int userId);

        #endregion

        #region Ekle, Sil

        Task AddQuestionAsync(AddQuestionVM addQuestionVM);

        Task UpdateQuestion(UpdateQuestionVM updateQuestionVM);

        Task DeleteQuestion(DeleteDto deleteDto);
 
        #endregion


    }
}
