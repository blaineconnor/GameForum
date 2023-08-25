namespace Game.Forum.UI.Models.DTOs.Vote
{
    public class AddVoteDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }
    }
}
