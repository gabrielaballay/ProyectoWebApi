using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class Recompensa
    {
        public int RecompensaId { get; set; }
        public Decimal Monto { get; set; }
        public int Tiempo { get; set; }
        public int Estado { get; set; }
    }
}
