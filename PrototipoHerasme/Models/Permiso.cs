using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            RolesPermisos = new HashSet<RolesPermiso>();
        }

        public int IdPermiso { get; set; }
        public string NombrePermiso { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<RolesPermiso> RolesPermisos { get; set; }
    }
}
