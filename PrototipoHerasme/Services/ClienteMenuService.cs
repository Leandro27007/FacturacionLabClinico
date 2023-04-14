using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PrototipoHerasme.Data;
using PrototipoHerasme.Models;
using PrototipoHerasme.Models.ViewModels;

namespace PrototipoHerasme.Services
{

    public class ClienteMenuService : IClienteMenuService
    {

        private DBHERASMEContext _context;
        public ClienteMenuService(DBHERASMEContext context)
        {
            this._context = context;
        }

        public async Task<ClienteServiciosViewModel> ListarServicios()
        {
            var listaServicios = await _context.Servicios.ToListAsync();

            ClienteServiciosViewModel model = new ClienteServiciosViewModel();
            model.Servicios = listaServicios;

            return model;
        }

        public async Task<string> GenerarTurnoAsync(int idServicio)
        {

            var cantidadTurnos = await _context.TurnoTemporals.CountAsync();

            if (cantidadTurnos > 100)
                cantidadTurnos = 0;

            TurnoTemporal turno = new TurnoTemporal();
            turno.IdTurno = cantidadTurnos + 1;
            turno.StadoTurno = "Pendiente";
            turno.IdServicio = idServicio;
            await _context.TurnoTemporals.AddAsync(turno);
            _context.SaveChanges();

            return turno.IdTurno.ToString();
        }




    }
}
