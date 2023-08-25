using Game.Forum.UI.Models.DTOs.User;

namespace Game.Forum.UI.Models.DTOs.Question
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
