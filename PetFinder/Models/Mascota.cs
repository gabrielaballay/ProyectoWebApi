using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        [Required]
        [Display(Name ="Nombre")]
        public string NombreMascota { get; set; }
        [Required]
        public string Raza { get; set; }
        [Required]
        [Display(Name = "Tamaño")]
        public string Tamanio { get; set; }
        [Required]
        public string Edad { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public string Imagen { get; set; }
        [NotMapped]
        public IFormFile ArchivoImagen { get; set; }
        public int Estado { get; set; }
        [Required]
        public string Fecha { get; set; }
        [ForeignKey("RecompensaId")]
        public Recompensa Insentivo { get; set; }
        [ForeignKey("UbicacionId")]
        public Ubicacion Lugar { get; set; }
        public int RecompensaId { get; set; }
        public int UbicacionId { get; set; }
        public int UsuarioId { get; set; }
    }
}
