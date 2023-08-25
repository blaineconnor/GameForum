namespace Game.Forum.Application.Models.DTOs.Vote
{
    public class AddVoteDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }
    }
}
