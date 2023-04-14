using PrototipoHerasme.Models.ViewModels;

namespace PrototipoHerasme.Services
{
    public interface IMedicoService
    {

        Task<PacientesViewModel> ObtenerPacienteAsync();
        Task<bool> AtenderPaciente(int IdFactura);
        Task<bool> CancelarPaciente(int? IdFactura);


    }
}
