using System;

namespace PetFinder.Models
{
    public class Recompensa
    {
        public int RecompensaId { get; set; }
        public Decimal Monto { get; set; }
        public string Tiempo { get; set; }
        public int Estado { get; set; }
    }
}
