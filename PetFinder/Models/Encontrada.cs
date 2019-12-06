using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Encontrada
    {
        public int EncontradaId { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Lugar { get; set; }
        public int UsuarioId { get; set; }
    }
}
