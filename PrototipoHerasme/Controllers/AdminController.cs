using Microsoft.AspNetCore.Mvc;
using PrototipoHerasme.Services;
namespace PrototipoHerasme.Controllers
{


    public class AdminController : Controller
    {

        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
        }



        public async Task<IActionResult> ListarReportes()
        {
           var resultado = await _adminService.ObtenerReportes();
            return View(resultado);
           

        }






    }
}
