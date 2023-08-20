using Game.Forum.Domain.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Game.Forum.Domain.Entities
{
    [JsonObject(IsReference = true)] // Döngüsel Referansları yönetmeye yardımcı olur

    public class Question : IEntity, ISoftDelete, IHasUpdatedAt
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Favorites = new HashSet<Favorite>();
            QuestionViews = new HashSet<QuestionView>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? UpdatedTime { get; set; }
        public string Content { get; set; } = null!;
        public bool IsDeleted { get; set; }

        #region NavigationProperties

        public virtual User User { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionView> QuestionViews { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Vote> Votes { get; set; }

        #endregion
    }
}
