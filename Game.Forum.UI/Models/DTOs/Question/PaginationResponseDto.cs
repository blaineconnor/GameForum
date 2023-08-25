namespace Game.Forum.UI.Models.DTOs.Question
{
    public class PaginationResponseDto<T> where T : class
    {
        public List<T> Data { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
