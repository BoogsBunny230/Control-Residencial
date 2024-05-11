using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PrivadasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public PrivadasController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]

        public ActionResult Index()
        {
            var privadas = _context.Privadas.AsQueryable();

            return View(privadas);
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
        public IActionResult Create(Privadas privadas)
        {
            var Exist = _contenedorTrabajo.Privadas.Get(privadas.IdPriv);
            if (Exist == null)
            {

                if (ModelState.IsValid)
                {
                    _contenedorTrabajo.Privadas.add(privadas);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(privadas);
            }
            //Mensaje de que ya existe el id
            return View();
        }

        //Borrar
        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(string id)
        {
            var objFromDb = _contenedorTrabajo.Privadas.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            _contenedorTrabajo.Privadas.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Registro eliminado correctamente" });

        }


        //Editar
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public IActionResult Edit(string id) //Cambie Int por String
        {
            Models.Privadas privadas = new Models.Privadas();
            privadas = _contenedorTrabajo.Privadas.Get(id);
            if (privadas == null)
            {
                return NotFound();
            }
            return View(privadas);
        }


        //Editar información
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(Models.Privadas privadas)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Privadas.Update(privadas);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(privadas);
        }


        #region Llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Privadas.GetAll() });
        }

        #endregion
    }
}
