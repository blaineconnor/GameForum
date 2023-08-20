using Game.Forum.Application.Models.DTOs.User;

namespace Game.Forum.Application.Models.DTOs.Answer
{
    public class AnswerResponseDto
    {
        public UserResponseDto User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Content { get; set; }

    }
}
