using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool? IsDeleted { get; set; }

        #region NavigationProperty

        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        #endregion
    }
}
