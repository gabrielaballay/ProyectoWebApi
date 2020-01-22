using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class ContactoViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public String Correo { get; set; }
        [Required]
        public string Asunto { get; set; }
        [Required]
        public string Mensaje { get; set; }
    }
}
