using Game.Forum.Application.Models.DTOs.User;

namespace Game.Forum.Application.Models.DTOs.Question
{
    public class GetAllQuestionsDto
    {
        public UserResponseDto User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public int Answer { get; set; }
    }

}
