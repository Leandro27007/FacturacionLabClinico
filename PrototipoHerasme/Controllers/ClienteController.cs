using Microsoft.AspNetCore.Mvc;
using PrototipoHerasme.Data;
using PrototipoHerasme.Models.ViewModels;
using PrototipoHerasme.Services;

namespace PrototipoHerasme.Controllers
{
    public class ClienteController : Controller
    {

        private IClienteMenuService _clienteMenuService;
        public ClienteController(IClienteMenuService clienteMenuService)
        {
            this._clienteMenuService  = clienteMenuService;
        }

        public async Task<IActionResult> ClienteMenu()
        {
            var model = await _clienteMenuService.ListarServicios();

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> GenerarTurno(int idServicio)
        {
            string turno = await _clienteMenuService.GenerarTurnoAsync(idServicio);
            return new ContentResult { Content = $"T-{turno}", ContentType = "text/plain"};
        }

    }
}
