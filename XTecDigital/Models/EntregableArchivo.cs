using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EntregableArchivo
    {
        public int Id { get; set; }
        public int IdEntregable { get; set; }
        public int IdArchivo { get; set; }

        public virtual Archivo IdArchivoNavigation { get; set; }
        public virtual Entregable IdEntregableNavigation { get; set; }
    }
}
