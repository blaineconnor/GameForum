namespace Game.Forum.UI.Models.RequestModels.Answers
{
    public class AddAnswerVM
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
    }
}
