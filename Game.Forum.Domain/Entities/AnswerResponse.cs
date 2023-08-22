using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class AnswerResponse : BaseEntity
    {
        public UserResponse User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Content { get; set; }
    }
}