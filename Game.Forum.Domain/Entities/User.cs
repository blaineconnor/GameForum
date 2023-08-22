using Game.Forum.Domain.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Game.Forum.Domain.Entities
{
    [JsonObject(IsReference = true)]
    public class User : AuditableEntity
    {

        public User()
        {
            Answers = new HashSet<Answer>();
            Favorites = new HashSet<Favorite>();
            QuestionViews = new HashSet<QuestionView>();
            Questions = new HashSet<Question>();
            Votes = new HashSet<Vote>();
        }

        public string Name { get; set; } 
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; } 
        public string? Image { get; set; }
        public Gender Gender { get; set; }

        #region NavigationProperties
        public Account Account { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<QuestionView> QuestionViews { get; set; }
        public ICollection<Question> Questions { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Vote> Votes { get; set; }

        #endregion

    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
    }
}
