using Microsoft.EntityFrameworkCore;
using PrototipoHerasme.Data;
using PrototipoHerasme.Models.ViewModels;

namespace PrototipoHerasme.Services
{
    public class MedicoService : IMedicoService
    {
        private DBHERASMEContext _context;

        public MedicoService(DBHERASMEContext context)
        {
            this._context = context;
        }


        public async Task<bool> AtenderPaciente(int IdFactura)
        {
            
            var factura = await _context.Facturas.Where(f => f.IdFactura ==  IdFactura).FirstOrDefaultAsync();
            if (factura == null)
                return false;
            factura.IdEstado = Constantes.facturaAtendida;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }  

        }

        public async Task<bool> CancelarPaciente(int? IdFactura)
        {

            var factura = await _context.Facturas.Where(f => f.IdFactura == IdFactura).FirstOrDefaultAsync();
            if (factura == null)
                return false;
            factura.IdEstado = Constantes.facturaCancelada;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<PacientesViewModel> ObtenerPacienteAsync()
        {
            var paciente = await _context.Facturas.Where(f => f.IdEstado == Constantes.facturaPendiente).Select(f => new
            {
                NombrePaciente = _context.Clientes.Where(c => c.IdCliente == f.IdCliente).Select(c => $"{c.NombreCliente} {c.ApellidoCliente}").FirstOrDefault(),
                Detalles = _context.DetalleFacturas.Where(d => d.IdFactura == f.IdFactura).Select(d => new
                {
                    Estudio = _context.Servicios.Where(s => s.IdServicio == d.IdServicio).Select(e => e.NombreServicio).FirstOrDefault()
                }).FirstOrDefault(),
                NoFactura = f.IdFactura
            }).FirstOrDefaultAsync();

            if (paciente == null)
                return new PacientesViewModel();


            //CON ESTA FORMA PUEDO RETORNAR UN OBJETO ANONIMO
            return new PacientesViewModel()
            {
                NombrePaciente = paciente.NombrePaciente,
                Estudio = paciente.Detalles.Estudio,
                NoFactura = paciente.NoFactura
            };

        }
    }
}
