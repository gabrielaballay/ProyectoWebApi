using System.ComponentModel.DataAnnotations;

namespace PetFinder.Models
{
    public class LoginView
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Clave { get; set; }
    }
}
