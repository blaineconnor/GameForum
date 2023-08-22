using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.Models.DTOs.User
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birtdate { get; set; }
        public Gender Gender { get; set; }
    }
}
