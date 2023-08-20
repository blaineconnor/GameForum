namespace Game.Forum.Domain.Entities
{
    public class PaginationResponse<T> where T : class
    {
        public List<T> Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}
