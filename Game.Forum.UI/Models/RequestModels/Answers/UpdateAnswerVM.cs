namespace Game.Forum.UI.Models.RequestModels.Answers
{
    public class UpdateAnswerVM
    {
        public int AnswerId { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
    }
}
