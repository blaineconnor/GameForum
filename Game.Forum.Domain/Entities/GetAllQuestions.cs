using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class GetAllQuestions : BaseEntity
    {
        public UserResponse User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Category { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public int Answer { get; set; }
    }
}
