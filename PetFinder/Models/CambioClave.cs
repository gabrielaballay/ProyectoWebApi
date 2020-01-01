using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class CambioClave
    {
        [DataType(DataType.Password)]
        [Required]
        public String OldClave { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{6,8}$",ErrorMessage = "Contraseña NO valida!!!")]
        public String NewClave { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{6,8}$", ErrorMessage = "Contraseña NO valida!!!")]
        public String RepeatClave { get; set; }
    }
}
