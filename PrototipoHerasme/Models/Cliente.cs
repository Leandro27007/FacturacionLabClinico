using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string? TelefonoCliente { get; set; }
        public string CedulaCliente { get; set; } = null!;
        public string? EmailCliente { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
