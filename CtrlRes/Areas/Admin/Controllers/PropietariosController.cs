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
    public class PropietariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public PropietariosController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]

        public ActionResult Index()
        {
            var propietarios = _context.Propietarios.AsQueryable();

            return View(propietarios);
        }


        //AGREGAR NUEVO PROPIETARIO***************************************************************************************

        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            return RedirectToAction("Create", "Viviendas", new { area = "Admin" });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create([Bind("NumViv, NomProp, Celular, MedAg, Vehiculo, Privada")] Propietarios propietarios)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Propietarios.add(propietarios);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Viviendas = _context.Viviendas.ToList();
                return View(propietarios);
            }
        }

        //******************************************************************************************************************

        //Borrar
        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(string id)
        {
            var objFromDb = _contenedorTrabajo.Propietarios.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            _contenedorTrabajo.Propietarios.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Registro eliminado correctamente" });

        }


        //Editar
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public IActionResult Edit(string id) //Cambie Int por String
        {
            Models.Propietarios propietarios = new Models.Propietarios();
            propietarios = _contenedorTrabajo.Propietarios.Get(id);
            if (propietarios == null)
            {
                return NotFound();
            }
            return View(propietarios);
        }


        //Editar información
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(Models.Propietarios propietarios)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Propietarios.Update(propietarios);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(propietarios);
        }


        #region Llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Propietarios.GetAll() });
        }


        #endregion
    }
}
