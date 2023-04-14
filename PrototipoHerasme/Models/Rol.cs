using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolesPermisos = new HashSet<RolesPermiso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<RolesPermiso> RolesPermisos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
