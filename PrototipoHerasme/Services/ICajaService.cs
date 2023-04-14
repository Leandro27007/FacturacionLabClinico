using PrototipoHerasme.Models.ViewModels;

namespace PrototipoHerasme.Services
{
    public interface ICajaService
    {


        Task<VentaViewModel> AtenderTurno();
        Task<bool> FacturarAsync(VentaViewModel model);
        Task<bool> Reembolsar(int idFactura);
        Task<bool> CancelarTurno(int idTurno);
        Task<ReembolsoViewModel> BuscarFactura(int idFactura);



    }
}
