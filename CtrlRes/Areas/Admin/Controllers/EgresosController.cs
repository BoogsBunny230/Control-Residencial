using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EgresosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EgresosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var egresos = _context.Egresos.ToList();

            ViewBag.Egresos = egresos;

            return View();
        }


        //Agregar nueva tarjeta Catalogo
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {

            var PrivadasList = _context.Privadas.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.IdPriv,
            });

            ViewBag.Privadas = PrivadasList;

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create(Egresos egresos)
        {

            if (ModelState.IsValid)
            {

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
                egresos.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

                _context.Egresos.Add(egresos);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(egresos);

        }
    }
}
