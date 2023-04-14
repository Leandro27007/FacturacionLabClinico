using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int IdServicio { get; set; }
        public string? NombreServicio { get; set; }
        public decimal? PrecioServicio { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
