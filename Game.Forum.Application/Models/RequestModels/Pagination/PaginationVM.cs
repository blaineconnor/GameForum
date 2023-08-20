using System.Text.Json.Serialization;

namespace Game.Forum.Application.Models.RequestModels.Pagination
{
    public class PaginationVM
    {
        [JsonIgnore]
        public int TotalPage { get; set; }
        [JsonIgnore]
        public int TotalData { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
