using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        public string NombreMascota { get; set; }
        public string  Raza { get; set; }
        public string  Tamaño { get; set; }
        public int Edad { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int Recompensa { get; set; }
        public int PerdidoEn { get; set; }
        public int UsuarioId { get; set; }
    }
}
