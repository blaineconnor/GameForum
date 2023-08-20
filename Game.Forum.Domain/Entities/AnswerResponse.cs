namespace Game.Forum.Domain.Entities
{
    public class AnswerResponse
    {
        public UserResponse User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Content { get; set; }
    }
}