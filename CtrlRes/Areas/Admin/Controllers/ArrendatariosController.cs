using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using CtrlRes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ArrendatariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public ArrendatariosController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;    
            _context = context; 
        }

        [HttpGet]

        public ActionResult Index()
        {
            var arrendatarios = _context.Arrendatarios.AsQueryable();

            return View(arrendatarios);
        }

        //Agregar nueva tarjeta Catalogo
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            ViewBag.Viviendas = _context.Viviendas.ToList();

            var arrendatarios = new Arrendatarios();
            return View(arrendatarios);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create(Arrendatarios arrendatarios)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Arrendatarios.add(arrendatarios);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Propietarios = _context.Propietarios.ToList();
                return View(arrendatarios);
            }
        }

        //Borrar
        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(string id)
        {
            var objFromDb = _contenedorTrabajo.Arrendatarios.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            _contenedorTrabajo.Arrendatarios.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Registro eliminado correctamente" });

        }


        //Editar
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public IActionResult Edit(string id) //Cambie Int por String
        {
            Models.Arrendatarios arrendatarios = new Models.Arrendatarios();
            arrendatarios = _contenedorTrabajo.Arrendatarios.Get(id);
            if (arrendatarios == null)
            {
                return NotFound();
            }
            return View(arrendatarios);
        }


        //Editar información
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(Models.Arrendatarios arrendatarios)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Arrendatarios.Update(arrendatarios);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(arrendatarios);
        }


        #region Llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Arrendatarios.GetAll() });
        }

        #endregion
    }
}
