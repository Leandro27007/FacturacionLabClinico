using System.ComponentModel.DataAnnotations;

namespace PrototipoHerasme.Models.ViewModels
{
    public class ClienteServiciosViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar al menos un servicio")]
        public Servicio ServicioSeleccionado { get; set; }
        public IEnumerable<Servicio> Servicios { get; set; }
    }
}
