using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class CarpetaRaiz
    {
        public int Id { get; set; }
        public int IdCarpeta { get; set; }

        public virtual Carpeta IdCarpetaNavigation { get; set; }
    }
}
