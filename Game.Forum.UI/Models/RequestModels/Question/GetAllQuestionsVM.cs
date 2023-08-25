using Game.Forum.UI.Models.DTOs.User;

namespace Game.Forum.UI.Models.RequestModels.Question
{
    public class GetAllQuestionsVM
    {
        public UserResponseDto User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Category { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public int Answer { get; set; }
    }
}
