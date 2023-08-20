using Game.Forum.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Game.Forum.Application.Models.RequestModels.Users
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailAgain { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirtDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }


        [Display(Name = "Kullanım koşullarını kabul ediyorum")]
        public bool Terms { get; set; }
    }
}
