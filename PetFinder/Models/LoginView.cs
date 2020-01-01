using System.ComponentModel.DataAnnotations;

namespace PetFinder.Models
{
    public class LoginView
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{4,8}$", ErrorMessage = "Contraseña NO valida!!!")]
        public string Clave { get; set; }
    }
}
