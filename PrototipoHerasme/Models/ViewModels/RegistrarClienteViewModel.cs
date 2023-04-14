using System.ComponentModel.DataAnnotations;

namespace PrototipoHerasme.Models.ViewModels
{
    public class RegistrarClienteViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El Nombre no puede contener más de 20 caracteres.")]
        public string NombreCliente { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [StringLength(20, ErrorMessage = "El Apellido no puede contener más de 20 caracteres.")]
        public string ApellidoCliente { get; set; }

        //Validar para que no me manden un numero con mas de 11 caracteres
        [StringLength(10, ErrorMessage = "El Telefono no puede contener más de 10 caracteres.")]
        public string? TelefonoCliente { get; set; }
        [Required(ErrorMessage = "La cedula es obligatoria")]
        [StringLength(6, ErrorMessage = "Este campo no puede contener más de 6 caracteres.")]
        public string CedulaCliente { get; set; }
        [EmailAddress]
        [StringLength(20, ErrorMessage = "El email no puede contener más de 20 caracteres.")]
        public string? EmailCliente { get; set; }
        public int turno { get; set; }
    }
}
