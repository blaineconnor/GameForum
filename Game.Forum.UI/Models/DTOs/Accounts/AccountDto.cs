using Game.Forum.UI.Models.DTOs.User;

namespace Game.Forum.UI.Models.DTOs.Accounts
{
    public class AccountDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastUserIp { get; set; }
        public Roles Role { get; set; }

        public UserDto User { get; set; }
        public UserContactDto UserContact { get; set; }
    }
}
