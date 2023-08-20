using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class QuestionView : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedTime { get; set; }

        #region NavigationProperties

        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        #endregion
    }
}
