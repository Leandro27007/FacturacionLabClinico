using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class EstadoFactura
    {
        public EstadoFactura()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdEstado { get; set; }
        public string? NombreEstado { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
