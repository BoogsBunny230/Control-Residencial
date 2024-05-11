using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HerramientasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public HerramientasController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Logotipo()
        {
            var herramienta = _context.Herramientas.FirstOrDefault(h => h.IdHerr == "Logotipo");
            return View(herramienta);
        }

        [HttpGet]
        public IActionResult ObtenerImagen()
        {
            var herramienta = _context.Herramientas.FirstOrDefault(h => h.IdHerr == "Logotipo");
            if (herramienta != null)
            {
                byte[] imageBytes = Convert.FromBase64String(herramienta.Contenido);
                return File(imageBytes, "image/jpeg");
                //return Json(herramienta.Contenido);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ActualizarLogotipo(string idHerr, string logotipo)
        {
            var herramienta = _context.Herramientas.FirstOrDefault(h => h.IdHerr == idHerr);
            if (herramienta != null)
            {
                herramienta.Contenido = logotipo;
                _context.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}