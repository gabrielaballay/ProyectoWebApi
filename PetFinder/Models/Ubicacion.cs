﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Models
{
    public class Ubicacion
    {
        public int UbicacionId { get; set; }
        [Required(ErrorMessage ="Ingrese Latitud")]
        public string Latitud { get; set; }
        [Required(ErrorMessage = "Ingrese Longitud")]
        public string Longitud { get; set; }
        [Required(ErrorMessage = "Ingrese una Zona")]
        public string Zona { get; set; }
    }
}
