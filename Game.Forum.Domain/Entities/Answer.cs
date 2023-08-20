using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Answer : IEntity, ISoftDelete, IHasUpdatedAt
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsBestAnswer { get; set; }


        #region NavigationProperties

        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        #endregion
    }
}
