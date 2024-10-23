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

using OfficeOpenXml;
using System.IO;


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

        public IActionResult Index(int? mes, int? ano)
        {
            var cantidadAlquileres = _context.DataPedidoAlquiler.Count();
            var cantidadVisitas = _context.DataPedidoVisita.Count();
            var cantidadEventos = _context.DataPedidoEvento.Count();

            mes = mes ?? DateTime.Now.Month;
            ano = ano ?? DateTime.Now.Year;

            var alquileres = _context.DataPedidoAlquiler
                .Where(a => a.Fecha.Month == mes && a.Fecha.Year == ano)
                .Count();
            var visitas = _context.DataPedidoVisita
                .Where(v => v.Fecha.Month == mes && v.Fecha.Year == ano)
                .Count();
            var eventos = _context.DataPedidoEvento
                .Where(e => e.Fecha.Month == mes && e.Fecha.Year == ano)
                .Count();

            var viewModel = new DashboardViewModel
            {
                CantidadAlquileres = _context.DataPedidoAlquiler.Count(),
                CantidadVisitas = _context.DataPedidoVisita.Count(),
                CantidadEventos = _context.DataPedidoEvento.Count(),
                FormAdmin = new FormAdminViewModel(),
                Mes = mes.Value,
                Ano = ano.Value
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
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value.ToUniversalTime())
                                && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value.ToUniversalTime()))
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
                            .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value.ToUniversalTime())
                                && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value.ToUniversalTime()))
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
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value.ToUniversalTime())
                                && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value.ToUniversalTime()))
                    .Select(p => new EventoViewModel
                    {
                        Id = p.IDPedidoEvento,
                        Fecha = p.Fecha,
                        Tipo = "Eventos",
                        Monto = p.PrecioTotal,
                        Cliente = p.Cliente,
                        Evento = p.Evento.ToString(),
                        Detalle = p.Detalle,
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

        [HttpPost]
        public IActionResult ExportToExcel(FormAdminViewModel formAdmin){
            List<VentaBase> ventasList = new List<VentaBase>();

            if (formAdmin.TipVenta == "alquileres")
            {
                var alquileres = _context.DataPedidoAlquiler
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value.ToUniversalTime())
                                && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value.ToUniversalTime()))
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
                            .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value.ToUniversalTime())
                                && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value.ToUniversalTime()))
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
                    .Where(p => (!formAdmin.FechaInicio.HasValue || p.Fecha >= formAdmin.FechaInicio.Value.ToUniversalTime())
                                && (!formAdmin.FechaFin.HasValue || p.Fecha <= formAdmin.FechaFin.Value.ToUniversalTime()))
                    .Select(p => new EventoViewModel
                    {
                        Id = p.IDPedidoEvento,
                        Fecha = p.Fecha,
                        Tipo = "Eventos",
                        Monto = p.PrecioTotal,
                        Cliente = p.Cliente,
                        Evento = p.Evento.ToString(),
                        Detalle = p.Detalle,
                        
                    }).ToList();
            }
             using (var package = new ExcelPackage()){
                var worksheet = package.Workbook.Worksheets.Add("Ventas");
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Fecha";
                worksheet.Cells[1, 3].Value = "Tipo";
                worksheet.Cells[1, 4].Value = "Monto";
                worksheet.Cells[1, 5].Value = "Cliente";
                worksheet.Cells[1, 6].Value = "Detalle";
                worksheet.Cells[1, 7].Value = "Cantidad";
                worksheet.Cells[1, 8].Value = "Precio Unitario";
                worksheet.Cells[1, 9].Value = "Guía";

                 var rowIndex = 2;
                 foreach (var venta in ventasList){
                    worksheet.Cells[rowIndex, 1].Value = venta.Id;
                    worksheet.Cells[rowIndex, 2].Value = venta.Fecha;
                    worksheet.Cells[rowIndex, 3].Value = venta.Tipo;
                    worksheet.Cells[rowIndex, 4].Value = venta.Monto;

                    if (venta is AlquilerViewModel alquiler)
                    {
                        worksheet.Cells[rowIndex, 5].Value = alquiler.Cliente?.ToString();
                        worksheet.Cells[rowIndex, 6].Value = alquiler.Alquiler;
                        worksheet.Cells[rowIndex, 7].Value = alquiler.CantPersona;
                        worksheet.Cells[rowIndex, 8].Value = ""; 
                        worksheet.Cells[rowIndex, 9].Value = ""; 
                    }
                    else if (venta is VisitaViewModel visita)
                    {
                        worksheet.Cells[rowIndex, 5].Value = visita.Cliente?.ToString();
                        worksheet.Cells[rowIndex, 6].Value = visita.Detalle;
                        worksheet.Cells[rowIndex, 7].Value = visita.Cantidad;
                        worksheet.Cells[rowIndex, 8].Value = visita.PrecioUnitario;
                        worksheet.Cells[rowIndex, 9].Value = visita.Guía?.ToString();
                    }
                    else if (venta is EventoViewModel evento)
                    {
                        worksheet.Cells[rowIndex, 5].Value = evento.Cliente?.ToString();
                        worksheet.Cells[rowIndex, 6].Value = evento.Detalle;
                        worksheet.Cells[rowIndex, 7].Value = evento.CantidadTotal;
                        worksheet.Cells[rowIndex, 8].Value = evento.PrecioTotal;
                        worksheet.Cells[rowIndex, 9].Value = "";
                    }

                    rowIndex++;

                }
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = "Ventas.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
                
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}