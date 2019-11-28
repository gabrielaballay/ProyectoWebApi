using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class Encontrada
    {
        public int EncontradaId { get; set; }
        public string Foto { get; set; }
        public string  Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int LocalizacionId { get; set; }
        public int MascotaId { get; set; }
        public int UsuarioId { get; set; }
    }
}
