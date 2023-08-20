namespace Game.Forum.Application.Models.DTOs.Question
{
    public class AddQuestionDto
    {
        public int UserId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
