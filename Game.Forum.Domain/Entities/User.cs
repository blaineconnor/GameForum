using Game.Forum.Domain.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Game.Forum.Domain.Entities
{
    [JsonObject(IsReference = true)]
    public class User : BaseEntity, IEntity, ISoftDelete, IHasUpdatedAt
    {

        public User()
        {
            Answers = new HashSet<Answer>();
            Favorites = new HashSet<Favorite>();
            QuestionViews = new HashSet<QuestionView>();
            Questions = new HashSet<Question>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public string? Image { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastUserIp { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Location { get; set; } = null!;
        public Gender Gender { get; set; }
        public Roles Role { get; set; }

        #region NavigationProperties
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<QuestionView> QuestionViews { get; set; }
        public ICollection<Question> Questions { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Vote> Votes { get; set; }

        #endregion

    }

    public enum Roles
    {
        User = 1,
        Admin = 2,
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
    }
}
