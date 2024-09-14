using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.EJ2.Charts;
using MUSEODESCALZOS.Data;
using MuseoDescalzos.Models;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using Microsoft.EntityFrameworkCore;
namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{
   
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var cantidadAlquileres = _context.DataPedidoAlquiler.Count();
            var cantidadVisitas = _context.DataPedidoVisita.Count();
            var cantidadEventos = _context.DataPedidoEvento.Count();

            var viewModel = new DashboardViewModel
            {
                CantidadAlquileres = _context.DataPedidoAlquiler.Count(),
                CantidadVisitas = _context.DataPedidoVisita.Count(),
                CantidadEventos = _context.DataPedidoEvento.Count(),
                FormAdmin = new FormAdminViewModel() 
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Mostrar(FormAdminViewModel formAdmin)
        {
            List<VentaBase> ventasList = new List<VentaBase>();

            if (formAdmin.TipVenta == "alquileres")
            {
                var alquileres = _context.DataPedidoAlquiler
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value)
                            && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value))
                    .Select(p => new AlquilerViewModel
                    {
                        Id = p.IDPedidoAlq,
                        Fecha = p.Fecha,
                        Tipo = "Alquileres",
                        Monto = p.PrecioTotal,
                        Cliente = p.Cliente,
                        Alquiler = p.Alquiler.ToString(),
                        HoraInicio = p.Hora_Inicio,
                        HoraFin = p.Hora_Fin,
                        CantPersona = p.CantPersona
                    }).ToList();
                
                ventasList.AddRange(alquileres.Cast<VentaBase>());
            }
            else if (formAdmin.TipVenta == "visitas")
            {
                var visitas = _context.DataPedidoVisita
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value)
                            && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value))
                    .Select(p => new VisitaViewModel
                    {
                        Id = p.IDPedidoVisit,
                        Fecha = p.Fecha,
                        Tipo = "Visitas",
                        Monto = p.PrecioTotal,
                        Cliente = p.Cliente,
                        Detalle = p.Detalle,
                        Cantidad = p.Cantidad,
                        PrecioUnitario = p.PrecioUnitario,
                        Guía = p.Guía
                    }).ToList();

                ventasList.AddRange(visitas.Cast<VentaBase>());
            }
            else if (formAdmin.TipVenta == "eventos")
            {
                var eventos = _context.DataPedidoEvento
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value)
                            && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value))
                    .Select(p => new EventoViewModel
                    {
                        Id = p.IDPedidoEvento,
                        Fecha = p.Fecha,
                        Tipo = "Eventos",
                        Monto = p.PrecioTotal,
                        Cliente = p.Cliente,
                        Evento = p.Evento.ToString(),
                        Detalle = p.Detalle,
                        Cantidad = p.Cantidad,
                        PrecioUnitario = p.PrecioUnitario
                    }).ToList();

                ventasList.AddRange(eventos.Cast<VentaBase>());
            }

            var viewModel = new DashboardViewModel
            {
                CantidadAlquileres = _context.DataPedidoAlquiler.Count(),
                CantidadVisitas = _context.DataPedidoVisita.Count(),
                CantidadEventos = _context.DataPedidoEvento.Count(),
                FormAdmin = formAdmin,
                ListVenta = ventasList
            };

            return View("Index", viewModel);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}