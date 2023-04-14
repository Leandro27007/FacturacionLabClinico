using Microsoft.EntityFrameworkCore;
using PrototipoHerasme.Data;
using PrototipoHerasme.Models;
using PrototipoHerasme.Models.ViewModels;
using System.Data.Common;

namespace PrototipoHerasme.Services
{
    public class CajaService : ICajaService
    {

        private DBHERASMEContext _context;
        public CajaService(DBHERASMEContext context)
        {
            this._context = context;
        }
        // para obtener el id usuario intentare crear una clase que me retorne el id dle usuario que esta logeado
        // pendiente a que pued evenir null
        public async Task<bool> FacturarAsync(VentaViewModel model)
        {

            /*Busco el servicio o producto segun el id recibido del backend, 
             * si me llega un id de alterado o que no exista, retorno falso.
             Si existe el producto sigo con el proceso, y uso este obj recibido
            para insertar el precio y el id del servicio en los detalles de la factura.*/
            var servicio = await _context.Servicios.Where(s => s.IdServicio == model.DetallesFactura.IdServicio).FirstOrDefaultAsync();
            if (servicio == null)
                return false;
            /*--*/


            Cliente objCliente = new();
            Factura objFactura = new();
            DetalleFactura objDetalleFactura = new();


            try
            {
                objCliente.NombreCliente = model.Cliente.NombreCliente;
                objCliente.ApellidoCliente = model.Cliente.ApellidoCliente;
                objCliente.CedulaCliente = model.Cliente.CedulaCliente;
                objCliente.TelefonoCliente = model.Cliente.TelefonoCliente;
                objCliente.EmailCliente = model.Cliente.EmailCliente;

                _context.Clientes.Add(objCliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return false;
            }


            try
            {
                objFactura.Descuento = model.Factura.Descuento;
                objFactura.IdUsuario = 4;//temporal, hasta poner Login y roles
                objFactura.IdEstado = 1;
                objFactura.IdCliente = objCliente.IdCliente;

                _context.Facturas.Add(objFactura);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                _context.Clientes.Remove(objCliente); //nota: CHEACKEAR POR QUE ESTO ME PRODUCE OTRA EXEPCION AL INTENTRAR BORRAR 
                await _context.SaveChangesAsync();


                return false;

            }


            try
            {
                objDetalleFactura.IdServicio = servicio.IdServicio;
                objDetalleFactura.PrecioServicio = servicio.PrecioServicio;
                objDetalleFactura.IdFactura = objFactura.IdFactura;



                _context.DetalleFacturas.Add(objDetalleFactura);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                _context.Facturas.Remove(objFactura);
                _context.Clientes.Remove(objCliente);
                await _context.SaveChangesAsync();

                return false;


            }

            /*Pendiente para poner en practica*/
            //objCliente.Facturas.Add(objFactura);
            //objFactura.DetalleFacturas.Add(objDetalleFactura);

            var turno = await _context.TurnoTemporals.Where(t => t.IdTurno == model.Cliente.turno).FirstOrDefaultAsync();
            if (turno != null)
            {
                turno.StadoTurno = "Atendido";
                await _context.SaveChangesAsync();

            }


            return true;
        }

        public async Task<VentaViewModel> AtenderTurno()
        {


            //Va en busca de un turno que este pendiente, si no encuentra ninguno retorna un nuevo OBJ vacio.
            var turno = await _context.TurnoTemporals.Where(turno => turno.StadoTurno == "Pendiente").FirstOrDefaultAsync();
            if (turno == null) { return new VentaViewModel(); }

            /*Optiene el servicio/producto donde con el id igual al que usuario selecciono para generar el turno,
             lo necesito para mandarlo a la vista, asi el usuario Cajer@ podra verlo a la hora de facturar, ayuda
            a la toma de deciciones. Igualmente si el cliente se equivoco al seleccionarlo el cajero podra editarlo, antes de guardar los cambios.
             */
            var servicio = await _context.Servicios.Where(servicio => servicio.IdServicio == turno.IdServicio).FirstOrDefaultAsync();

            VentaViewModel venta = new VentaViewModel();


            //Retorna el objeto solo con el mensaje de error, que indica que ese servicio no tiene precio por lo cual no puede ser despachado.
            if (servicio == null || servicio.PrecioServicio == null)
            {
                venta.MensajeEstado = "No se encontro o no se ha asignado un precio al servicio, contacte a un administrador para mas detalles";
                return venta;
            }

            venta.Cliente.turno = turno.IdTurno;
            venta.DetallesFactura.IdServicio = servicio.IdServicio;
            venta.DetallesFactura.PrecioServicio = servicio.PrecioServicio;
            venta.DetallesFactura.NombreServico = servicio.NombreServicio;
            venta.DetallesFactura.DescripcionServicio = servicio.Descripcion;
            return venta;
        }

        public async Task<bool> Reembolsar(int idFactura)
        {
            /*ESTADOS DE UNA FACTURA:  Reembolsado= 4 /Completamente atendida= 3  /Espera de Resultados= 2 /Estudios pendiente 1*/

            var factura = await _context.Facturas.Where(f => (f.IdFactura == idFactura && f.IdEstado == 1) || (f.IdEstado == Constantes.facturaCancelada) ).FirstOrDefaultAsync();
            if (factura == null)
                return false;

            factura.IdEstado = 4;
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

        public async Task<bool> CancelarTurno(int idTurno)
        {
            var turno = await _context.TurnoTemporals.Where(t => t.IdTurno == idTurno).FirstOrDefaultAsync();

            if (turno == null)
                return false;

            turno.StadoTurno = "Cancelado";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public async Task<ReembolsoViewModel> BuscarFactura(int idFactura)
        {
            int idCliente = await _context.Facturas.Where(f => f.IdFactura == idFactura).Select(f => f.IdCliente).FirstOrDefaultAsync();

            var objFacturaCliente = await _context.Clientes.Where(c => c.IdCliente == idCliente).Select(c => new
            {
                NombreCompleto = $"{c.NombreCliente} {c.ApellidoCliente}",
                Telefono = c.TelefonoCliente,
                Cedula = c.CedulaCliente,
                DetallesFactura = c.Facturas.SelectMany(f => f.DetalleFacturas.Select(d => new
                {
                    PrecioServicio = d.PrecioServicio,
                    IdFactura = d.IdFactura,
                    NombreServicio = _context.Servicios.Where(s => s.IdServicio == d.IdServicio).Select(s => s.NombreServicio).FirstOrDefault()
                }))
            }).FirstOrDefaultAsync();

            if (objFacturaCliente == null)
                return new ReembolsoViewModel();


            ReembolsoViewModel reembolsoViewModel = new ReembolsoViewModel();


            reembolsoViewModel.NombreCompletoCliente = objFacturaCliente.NombreCompleto;
            reembolsoViewModel.TelefonoCliente = objFacturaCliente.Telefono;
            reembolsoViewModel.CedulaCliente = objFacturaCliente.Cedula;
            reembolsoViewModel.PrecioServicio = objFacturaCliente.DetallesFactura.FirstOrDefault().PrecioServicio;
            reembolsoViewModel.IdFactura = objFacturaCliente.DetallesFactura.FirstOrDefault().IdFactura;
            reembolsoViewModel.NombreServicio = objFacturaCliente.DetallesFactura.FirstOrDefault().NombreServicio;


            return reembolsoViewModel;

            /* ANTIGUA FORMA, CON 4 CONSULTAS A DB
        
            var facturaCliente = await _context.Facturas.Where(f => f.IdFactura == idFactura).FirstOrDefaultAsync();
          
            var modelDetalles = await _context.DetalleFacturas.Where(d => d.IdFactura == idFactura).FirstOrDefaultAsync();

            if (modelDetalles == null)
                return new ReembolsoViewModel();


            var modelCliente = await _context.Clientes.Where(c => c.IdCliente == modelFactura.IdCliente).FirstOrDefaultAsync();
            var Servicio = await _context.Servicios.Where(s => s.IdServicio == modelDetalles.IdServicio).FirstOrDefaultAsync();

            objDatosReembolso.NombreServicio = Servicio.NombreServicio;
            objDatosReembolso.NombreCompletoCliente = $"{modelCliente.NombreCliente} {modelCliente.ApellidoCliente}";
            objDatosReembolso.TelefonoCliente = modelCliente.TelefonoCliente;     
            objDatosReembolso.CedulaCliente = modelCliente.CedulaCliente;
            objDatosReembolso.PrecioServicio = modelDetalles.PrecioServicio;
            objDatosReembolso.IdFactura = modelDetalles.IdFactura;
            */


        }
    }
}
