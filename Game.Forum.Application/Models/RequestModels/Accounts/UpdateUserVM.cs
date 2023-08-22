namespace Game.Forum.Application.Models.RequestModels.Accounts
{
    public class UpdateUserVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
