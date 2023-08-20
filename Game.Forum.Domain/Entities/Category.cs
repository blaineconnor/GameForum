using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Category : BaseEntity, IEntity, IHasUpdatedAt, ISoftDelete
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        DateTime? IHasUpdatedAt.UpdatedTime { get; set; }

        #region NavigationProperties
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
       
        #endregion
    }
}
