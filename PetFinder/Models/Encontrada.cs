using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Encontrada
    {
        public int EncontradaId { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public string Imagen { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Fecha { get; set; }
        public int Estado { get; set; }
        [ForeignKey("UbicacionId")]
        public Ubicacion Lugar { get; set; }
        public int UbicacionId { get; set; }
        public int UsuarioId { get; set; }
    }
}
