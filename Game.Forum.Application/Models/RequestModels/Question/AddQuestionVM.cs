namespace Game.Forum.Application.Models.RequestModels.Question
{
    public class AddQuestionVM
    {
        public int UserId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
