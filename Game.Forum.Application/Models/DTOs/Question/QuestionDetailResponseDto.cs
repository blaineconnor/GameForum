using Game.Forum.Application.Models.DTOs.Answer;
using Game.Forum.Application.Models.DTOs.Category;
using Game.Forum.Application.Models.DTOs.User;

namespace Game.Forum.Application.Models.DTOs.Question
{
    public class QuestionDetailResponseDto
    {
        public UserResponseDto User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CategoryName { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public int Answer { get; set; }
        public int Favorite { get; set; }
        public bool IsFavorite { get; set; }
        public IEnumerable<AnswerResponseDto> AnswerResponse { get; set; }
        public IEnumerable<CategoryDto> CategoryDto { get; set; }
    }
}
