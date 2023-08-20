using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Vote : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }

        #region NavigationProperties
        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        #endregion
    }
}
