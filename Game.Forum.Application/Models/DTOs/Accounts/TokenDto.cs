using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.Models.DTOs.Accounts
{
    public class TokenDto
    {
        public Roles Role { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
