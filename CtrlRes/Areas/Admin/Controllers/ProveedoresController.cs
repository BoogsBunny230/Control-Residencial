using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProveedoresController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public ProveedoresController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]

        public ActionResult Index()
        {
            var proveedores = _context.Proveedores.AsQueryable();

            return View(proveedores);
        }

        //Agregar nueva tarjeta Catalogo
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create(Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Proveedores.add(proveedores);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(proveedores);
            }
        }

        //Borrar
        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(string id)
        {
            var objFromDb = _contenedorTrabajo.Proveedores.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            _contenedorTrabajo.Proveedores.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Registro eliminado correctamente" });

        }


        //Editar
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public IActionResult Edit(string id) //Cambie Int por String
        {
            Models.Proveedores proveedores = new Models.Proveedores();
            proveedores = _contenedorTrabajo.Proveedores.Get(id);
            if (proveedores == null)
            {
                return NotFound();
            }
            return View(proveedores);
        }


        //Editar información
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(Models.Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Proveedores.Update(proveedores);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(proveedores);
        }


        #region Llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Proveedores.GetAll() });
        }

        #endregion
    }
}

