using System;
using System.Collections.Generic;

namespace PrototipoHerasme.Models
{
    public partial class TurnoTemporal
    {
        public int IdTurno { get; set; }
        public string StadoTurno { get; set; } = null!;
        public int? IdServicio { get; set; }
    }
}
