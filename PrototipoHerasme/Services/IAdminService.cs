using PrototipoHerasme.Models.ViewModels;

namespace PrototipoHerasme.Services
{
    public interface IAdminService
    {
        Task<List<ReportesViewModel>> ObtenerReportes();

    }
}
