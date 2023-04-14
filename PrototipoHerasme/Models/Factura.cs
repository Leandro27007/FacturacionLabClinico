using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int IdFactura { get; set; }
        public string? Sucursal { get; set; }
        public decimal? Descuento { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public byte[] FechaFactura { get; set; } = null!;

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual EstadoFactura IdEstadoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
