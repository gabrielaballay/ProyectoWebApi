using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class CambiarCorreo
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public String OldCorreo { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public String NewCorreo { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public String RepeatCorreo { get; set; }
    }
}
