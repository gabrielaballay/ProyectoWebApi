using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        public string NombreMascota { get; set; }
        public string Raza { get; set; }
        public string Tamanio { get; set; }
        public int Edad { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public string Imagen { get; set; }
        public int Estado { get; set; }
        public string Fecha { get; set; }
        public int RecompensaId { get; set; }
        //[NotMapped]
        //public Recompensa Insentivo { get; set; }
        public String Lugar { get; set; }
        public int UsuarioId { get; set; }
    }
}
