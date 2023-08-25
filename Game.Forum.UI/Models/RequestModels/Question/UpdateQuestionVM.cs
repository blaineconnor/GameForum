namespace Game.Forum.UI.Models.RequestModels.Question
{
    public class UpdateQuestionVM
    {
        public int QuestionId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
