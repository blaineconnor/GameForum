using Game.Forum.Domain.Common;

namespace Game.Forum.Domain.Entities
{
    public class UserResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image { get; set; }
        public int Id { get; set; }

    }
}
