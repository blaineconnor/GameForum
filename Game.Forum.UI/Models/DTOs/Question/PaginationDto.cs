using System.Text.Json.Serialization;

namespace Game.Forum.UI.Models.DTOs.Question
{
    public class PaginationDto
    {
        [JsonIgnore]
        public int TotalPage { get; set; }
        [JsonIgnore]
        public int TotalData { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }


    }
}
