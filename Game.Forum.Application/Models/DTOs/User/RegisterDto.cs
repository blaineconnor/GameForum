namespace Game.Forum.Application.Models.DTOs.User
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Location { get; set; }
    }
}
