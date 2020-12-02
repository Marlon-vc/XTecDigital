using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EntregableArchivoDetalle
    {
        public int Id { get; set; }
        public int IdEntregable { get; set; }
        public int IdArchivoDetalle { get; set; }

        public virtual Archivo IdArchivoDetalleNavigation { get; set; }
        public virtual Entregable IdEntregableNavigation { get; set; }
    }
}
