namespace Game.Forum.UI.Models.DTOs.Accounts
{
    public class TokenDto
    {
        public Roles Role { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
