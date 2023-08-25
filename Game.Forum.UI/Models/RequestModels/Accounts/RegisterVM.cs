namespace Game.Forum.UI.Models.RequestModels.Accounts
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string EmailAgain { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }

    }
}
