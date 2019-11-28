using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class Localizacion
    {
        public int LocalizacionId { get; set; }
        public Decimal Latitud { get; set; }
        public Decimal Longitud { get; set; }
    }
}
