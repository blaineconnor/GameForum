using System.ComponentModel.DataAnnotations;

namespace Game.Forum.UI.Models.RequestModels.Categories
{
    public class CreateCategoryVM
    {
        [Display(Name = "Kategori adı")]
        public string CategoryName { get; set; }
    }
}
