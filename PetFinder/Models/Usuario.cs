using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required]
        public string Telefono { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Clave { get; set; }
        public int Estado { get; set; }
        [Display(Name ="Provincia")]
        public int ProvinciaId { get; set; }
        [ForeignKey("ProvinciaId")]
        public Provincia Vive { get; set; }
    }
}
