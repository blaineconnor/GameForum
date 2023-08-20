namespace Game.Forum.Application.Models.DTOs.Answer
{
    public class AddAnswerDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
    }
}
