using Microsoft.AspNetCore.Mvc;
using PrototipoHerasme.Models;
using PrototipoHerasme.Services;

namespace PrototipoHerasme.Controllers
{
    public class MedicoController : Controller
    {

        private IMedicoService _medicoService;
        public MedicoController(IMedicoService medicoService)
        {
            this._medicoService = medicoService;
        }


        public async Task<IActionResult> Index()
        {
            var paciente = await _medicoService.ObtenerPacienteAsync();

            return View(paciente);
        }


        [HttpPost]
        public async Task<IActionResult> CancelarPaciente(int idFactura)
        {

            var resultado = await _medicoService.CancelarPaciente(idFactura);
            return new ContentResult { Content = resultado.ToString()+"Cancelado", ContentType = "text/plain" };
        }


        [HttpPost]
        public async Task<IActionResult> DespacharPaciente(int idFactura)
        {
            var resultado = await _medicoService.AtenderPaciente(idFactura);
            return new ContentResult { Content = resultado.ToString()+"Despachado", ContentType = "text/plain" };
        }


    }
}
