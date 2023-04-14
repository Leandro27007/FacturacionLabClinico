using PrototipoHerasme.Models;
using PrototipoHerasme.Models.ViewModels;

namespace PrototipoHerasme.Services
{
    public interface IClienteMenuService
    {
        Task<ClienteServiciosViewModel> ListarServicios();
        Task<string> GenerarTurnoAsync(int idServicio);


    }
}
