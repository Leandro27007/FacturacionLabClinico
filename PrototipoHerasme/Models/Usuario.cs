using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdUsuario { get; set; }
        public string Username { get; set; } = null!;
        public string Pwd { get; set; } = null!;
        public string? Correo { get; set; }
        public byte[] FechaRegistro { get; set; } = null!;
        public string? Imagen { get; set; }
        public int? IdRole { get; set; }
        public string? NombreUsuario { get; set; }
        public string? ApellidoUsuario { get; set; }

        public virtual Rol? IdRoleNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
