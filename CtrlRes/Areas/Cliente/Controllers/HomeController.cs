using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CtrlRes.Areas.Cliente.Controllers
{
    [Authorize]
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var ingresosData = _context.Pagos
                .Where(p => p.Estado == "Aprobado")
                .Select(p => new {
                    Fecha = p.FechaRegistroPago,
                    Monto = p.Monto,
                    Referencia = p.Referencia,
                    ViviendaId = p.Vivienda_Id,
                    Concepto = p.Concepto
                })
                .ToList();

            var minDate = ingresosData.Min(p => p.Fecha);

            var egresosData = _context.Egresos
                .Select(e => new { 
                    Fecha = e.FechaRegistro,
                    Monto = e.Monto,
                    Tipo = e.Tipo,
                    Concepto = e.Concepto,
                    PrivadaId = e.Privada_Id
                })
                .ToList();

            ViewBag.IngresosData = ingresosData;
            ViewBag.MinDate = minDate;
            ViewBag.EgresosData = egresosData;

            //

            var sinPagar = _context.Pagos.Where(p => p.Estado == "Sin Pagar").Count();
            var aprobado = _context.Pagos.Where(p => p.Estado == "Aprobado").Count();
            var rechazado = _context.Pagos.Where(p => p.Estado == "Rechazado").Count();

            ViewBag.SinPagar = sinPagar;
            ViewBag.Aprobado = aprobado;
            ViewBag.Rechazado = rechazado;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}