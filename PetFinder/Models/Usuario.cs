using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [Required]
        [MinLength(2,ErrorMessage ="Debe ingresar al menos 2 caracteres")]
        public string Apellido { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Debe ingresar al menos 2 caracteres")]
        public string Nombre { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required] /*VER COMENTARIO ABAJO*/
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:(?:00)?549?)?0?(?:11|[2368]\d)(?:(?=\d{0,2}15)\d{2})??\d{8}$",ErrorMessage ="Ingrese un Telefono Valido")]
        public string Telefono { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Ingrese un mail valido!")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{6,8}$", ErrorMessage = "Contraseña NO valida!!!")]
        public string Clave { get; set; }
        public int Estado { get; set; }
        [Display(Name ="Provincia")]
        public int ProvinciaId { get; set; }
        [ForeignKey("ProvinciaId")]
        public Provincia Vive { get; set; }

        /*****************************************************************************
         *          Expresion regular para nro de Telefono
         * Toma como opcionales:
         *  el prefijo internacional (54)
         *  el prefijo internacional para celulares (9)
         *  el prefijo de acceso a interurbanas (0)
         *  el prefijo local para celulares (15)
         *  Es obligatorio:
         *  el código de área (11, 2xx, 2xxx, 3xx, 3xxx, 6xx y 8xx)
         *  (no toma como válido un número local sin código de área como 4444-0000)
         */
    }
}
