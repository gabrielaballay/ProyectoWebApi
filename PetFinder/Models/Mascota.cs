using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        [Required]
        public string NombreMascota { get; set; }
        [Required]
        public string Raza { get; set; }
        [Required]
        public string Tamanio { get; set; }
        [Required]
        public int Edad { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public string Imagen { get; set; }
        public int Estado { get; set; }
        [Required]
        public string Fecha { get; set; }
        public int RecompensaId { get; set; }
        //[NotMapped]
        //public Recompensa Insentivo { get; set; }
        [Required]
        public String Lugar { get; set; }
        public int UsuarioId { get; set; }
    }
}
