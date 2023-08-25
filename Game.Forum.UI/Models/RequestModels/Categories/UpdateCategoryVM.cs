using System.ComponentModel.DataAnnotations;

namespace Game.Forum.UI.Models.RequestModels.Categories
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }

        [Display(Name = "Kategori adı")]
        public string CategoryName { get; set; }
    }
}
