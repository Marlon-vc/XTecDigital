﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTecDigital.Models.Requests;

namespace XTecDigital.Models.Mongo
{
    public class Estudiante
    {
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Pass { get; set; }
    }
}
