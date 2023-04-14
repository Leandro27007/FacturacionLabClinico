using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public decimal? PrecioServicio { get; set; }
        public int? IdFactura { get; set; }
        public int? IdServicio { get; set; }

        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual Servicio? IdServicioNavigation { get; set; }
    }
}
