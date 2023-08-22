using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class Account : BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastUserIp { get; set; }
        public Roles Role { get; set; }

        public User User { get; set; }
    }

    public enum Roles
    {
        User = 1,
        Admin = 2
    }
}
