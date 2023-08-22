using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        #region NavigationProperties
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
       
        #endregion
    }
}
