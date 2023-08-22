using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Vote : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }

        #region NavigationProperties
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }

        #endregion
    }
}
