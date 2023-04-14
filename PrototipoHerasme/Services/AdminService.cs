using Microsoft.EntityFrameworkCore;
using PrototipoHerasme.Data;
using PrototipoHerasme.Models.ViewModels;
using System.Data.Common;

namespace PrototipoHerasme.Services
{
    public class AdminService : IAdminService
    {


        private DBHERASMEContext _context;
        public AdminService(DBHERASMEContext context)
        {
            this._context = context;
        }

        public async Task<List<ReportesViewModel>> ObtenerReportes()
        {

            try
            {
                var listaReportes = await _context.Facturas.Select(f => new
                {
                    IdFactura = f.IdFactura,
                    NombreCliente = _context.Clientes.Where(c => c.IdCliente == f.IdCliente).Select(c => c.NombreCliente).FirstOrDefault(),
                    NombreUsuario = _context.Usuarios.Where(u => u.IdUsuario == f.IdUsuario).Select(u => $"{u.NombreUsuario} {u.ApellidoUsuario}").FirstOrDefault(),
                    Precioservicio = _context.DetalleFacturas.Where(d => d.IdFactura == f.IdFactura).Select(d => d.PrecioServicio).FirstOrDefault(),
                    NombreServicio = _context.Servicios.Where(s => s.IdServicio == _context.DetalleFacturas.Where(d=> d.IdFactura== f.IdFactura).Select(d=> d.IdServicio).FirstOrDefault()).Select(s => s.NombreServicio).FirstOrDefault() 
                    //ESTA LINEA DE CODIGO LA PUSE ASI, POR QUE ME DA?BA UNA EXEPCION AL HACER UN OBJETO
                    //ANONIMO DE DETALLES DENTRO DE ESTA CONSULTA.
                }).ToListAsync();


                if (listaReportes == null)
                    return new List<ReportesViewModel>();


                var objReportesViwModel = new List<ReportesViewModel>();

                foreach (var item in listaReportes)
                {
                    objReportesViwModel.Add(new ReportesViewModel()
                    {
                        IdFactura = item.IdFactura,
                        NombreCliente = item.NombreCliente,
                        NombreUsuario = item.NombreUsuario,
                        NombreServicio = item.NombreServicio,
                        Precio = item.Precioservicio,
                    });
                }
              


                return objReportesViwModel;


            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
