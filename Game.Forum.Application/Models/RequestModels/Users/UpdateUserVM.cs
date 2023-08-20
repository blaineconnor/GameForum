namespace Game.Forum.Application.Models.RequestModels.Users
{
    public class UpdateUserVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
