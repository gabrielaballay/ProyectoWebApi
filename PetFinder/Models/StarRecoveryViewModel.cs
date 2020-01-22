using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class StarRecoveryViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public String Correo { get; set; }
    }

    public class RecoveryViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public String Correo { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{6,8}$", ErrorMessage = "Contraseña NO valida!!!")]
        public String NewClave { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{6,8}$", ErrorMessage = "Contraseña NO valida!!!")]
        public String RepeatClave { get; set; }
    }
}
