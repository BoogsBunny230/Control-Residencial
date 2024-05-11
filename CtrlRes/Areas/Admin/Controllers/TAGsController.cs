using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TAGsController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public TAGsController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]

        public ActionResult Index(string buscar, string ordenarPor)
        {
            var tAGs = _context.TAGs.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                tAGs = tAGs.Where(p => p.TAG.Contains(buscar));
            }

            switch (ordenarPor)
            {
                case "TAG":
                    tAGs = tAGs.OrderBy(p => p.TAG);
                    break;
                case "Propietario":
                    tAGs = tAGs.OrderBy(p => p.Propietario);
                    break;
                case "Privada":
                    tAGs = tAGs.OrderBy(p => p.Privada);
                    break;
                default:
                    break;
            }

            return View(tAGs);
        }

        //Agregar nueva TAG*****************************************************************

        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            ViewBag.Propietarios = _context.Propietarios.ToList();
            ViewBag.Privadas = _context.Privadas.ToList();

            var tAGs = new TAGs();
            return View(tAGs);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create(TAGs tAGs)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.TAGs.add(tAGs);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Propietarios = _context.Propietarios.ToList();
                ViewBag.Privadas = _context.Privadas.ToList();
                return View(tAGs);
            }
        }

        //***********************************************************************************

        //Borrar
        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(string id)
        {
            var objFromDb = _contenedorTrabajo.TAGs.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            _contenedorTrabajo.TAGs.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Registro eliminado correctamente" });

        }


        //Editar
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public IActionResult Edit(string id) //Cambie Int por String
        {
            Models.TAGs tAGs = new Models.TAGs();
            tAGs = _contenedorTrabajo.TAGs.Get(id);
            if (tAGs == null)
            {
                return NotFound();
            }
            return View(tAGs);
        }


        //Editar información
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(Models.TAGs tAGs)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.TAGs.Update(tAGs);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(tAGs);
        }


        #region Llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.TAGs.GetAll() });
        }

        #endregion
    }
}