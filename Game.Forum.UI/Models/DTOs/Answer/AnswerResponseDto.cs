using Game.Forum.UI.Models.DTOs.User;

namespace Game.Forum.UI.Models.DTOs.Answer
{
    public class AnswerResponseDto
    {
        public UserResponseDto User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Content { get; set; }

    }
}
