using Microsoft.AspNetCore.Mvc;
using PrototipoHerasme.Models.ViewModels;
using PrototipoHerasme.Services;

namespace PrototipoHerasme.Controllers
{
    public class CajaController : Controller
    {

        private ICajaService _cajaService;

        public CajaController(ICajaService cajaService)
        {
            this._cajaService = cajaService;
        }


        public async Task<IActionResult> Vender()
        {
            //Ira a la capa de Service y regresara un obj con todos los datos necesario para registrar una venta. Si hay un turno pendiente lo trae, de lo contrario trael el obj con los campos en Null
            var venta = await _cajaService.AtenderTurno();

            if (venta.Cliente.turno < 1)
                TempData["mensaje"] = "Mensaje 3";

            return View(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Vender(VentaViewModel modelo)
        {

            if (!ModelState.IsValid)
                return View("Vender", modelo);


            bool resultado = await _cajaService.FacturarAsync(modelo);

            if (!resultado)
            {
                TempData["mensaje"] = "Mensaje 2";
                return View(modelo);

            }

            TempData["mensaje"] = "Mensaje 1";

            return RedirectToAction("Vender", TempData["mensajeResultado"]);
        }


        public IActionResult Reembolsar(VentaViewModel? model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reembolsar(int idFactura)
        {
            bool resultado = await _cajaService.Reembolsar(idFactura);

            return new ContentResult { Content = resultado.ToString(), ContentType = "text/plain" };


        }


        [HttpPost]
        public async Task<IActionResult> CancelarTurno(int idTurno)
        {
            bool resultado = await _cajaService.CancelarTurno(idTurno);

            return new ContentResult { Content = resultado.ToString(), ContentType = "text/plain" };
        }

        [HttpPost]
        public async Task<IActionResult> BuscarFactura(int idFactura)
        {

            ReembolsoViewModel modelo = await _cajaService.BuscarFactura(idFactura);

            return View("Reembolsar",modelo);
        }

    }
}
