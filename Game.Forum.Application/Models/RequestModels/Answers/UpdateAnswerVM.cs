namespace Game.Forum.Application.Models.RequestModels.Answers
{
    public class UpdateAnswerVM
    {
        public int AnswerId { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
    }
}
