using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        public string NombreMascota { get; set; }
        public string  Raza { get; set; }
        public string  Tamanio { get; set; }
        public int Edad { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
		[NotMapped]
		public IFormFile Imagen { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int RecompensaId { get; set; }
        public int LocalizaId { get; set; }
        public int UsuarioId { get; set; }
    }
}
