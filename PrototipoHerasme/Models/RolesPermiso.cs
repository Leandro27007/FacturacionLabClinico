using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class RolesPermiso
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public int IdPermiso { get; set; }

        public virtual Permiso IdPermisoNavigation { get; set; } = null!;
        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}
