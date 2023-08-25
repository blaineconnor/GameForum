using AutoMapper;

namespace Game.Forum.Domain.Entities
{
    public class AddQuestionToFav
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public Favorite Favorite { get; set; }
        
    }
}
