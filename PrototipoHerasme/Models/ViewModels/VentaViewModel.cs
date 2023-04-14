namespace PrototipoHerasme.Models.ViewModels
{
    public class VentaViewModel
    {
        public RegistrarClienteViewModel Cliente { get; set; } = new RegistrarClienteViewModel(); // new RegistrarClienteViewModel(); Esta linea la necesitaba si tenia que asignar un valor a un campo de esta propiedad, pero ya no la ocupo. APUNTE: necesitaba instanciar esa propiedad para poder usarla una vez haceia la instancia de esta clase, Ese dato no lo sabia. Como podira ahacer esto de una forma mas optima?      
      //  public IEnumerable<DetallesViewModel> DetallesFactura { get; set; } // nota: SI QUIERO CUMPLIR CON AGREGAR MUCHOS DETALLES A UNA FACTURA
        public DetallesViewModel DetallesFactura { get; set; } = new();
        public FacturaClienteViewModel Factura { get; set; } = new();

        public string? MensajeEstado { get; set; }

    }
}
