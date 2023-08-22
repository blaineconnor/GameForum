using Game.Forum.Application.Models.DTOs.User;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.Models.DTOs.Accounts
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
    }
}
