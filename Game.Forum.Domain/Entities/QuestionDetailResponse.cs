namespace Game.Forum.Domain.Entities
{
    public class QuestionDetailResponse
    {
        public UserResponse User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Category { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public int Answer { get; set; }
        public int Favorite { get; set; }
        public bool IsFavorite { get; set; }
        public IEnumerable<AnswerResponse> AnswerResponse { get; set; }
    }
}
