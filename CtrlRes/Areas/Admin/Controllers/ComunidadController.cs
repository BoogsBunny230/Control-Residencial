using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CtrlRes.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class ComunidadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Viviendas? vivienda;
        private Privadas? privada;

        public ComunidadController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Comunidad.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunidad = await _context.Comunidad.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.Id == id);
            if (comunidad == null)
            {
                return NotFound();
            }

            comunidad.Visto = true;
            _context.Update(comunidad);
            await _context.SaveChangesAsync();

            if(comunidad.ApplicationUser.UsuarioTipo == "Propietario") 
            {
                var vivienda_Id =  _context.Propietarios.FirstOrDefault(m => m.Id == comunidad.ApplicationUser.PropOrArr_Id).Vivienda_Id;
                vivienda = _context.Viviendas.FirstOrDefault(m => m.Id == vivienda_Id);
                privada = _context.Privadas.FirstOrDefault(m => m.IdPriv == vivienda.Privada_Id);
            }


            ViewBag.Message = comunidad;
            ViewBag.Vivienda = vivienda;
            ViewBag.Privada = privada;
            return View("Index", await _context.Comunidad.ToListAsync());
        }

        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunidad = await _context.Comunidad.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.Id == id);
            if (comunidad == null)
            {
                return NotFound();
            }

            comunidad.Oculto = true;
            _context.Update(comunidad);
            await _context.SaveChangesAsync();

            return View("Index", await _context.Comunidad.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int id, string respuesta)
        {
            var comunidad = await _context.Comunidad.FirstOrDefaultAsync(m => m.Id == id);
            if (comunidad == null)
            {
                return NotFound();
            }

            comunidad.Respondido = true;
            comunidad.Respuesta = respuesta;
            comunidad.FechaRegistroRespuesta = DateTime.Now.ToString();
            _context.Update(comunidad);
            await _context.SaveChangesAsync();

            ViewBag.Message = comunidad;
            return View("Index", await _context.Comunidad.ToListAsync());
        }


    }
}
