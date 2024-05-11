using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PagosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pagos
        public IActionResult Index()
        {
            // Obtener el listado de privadas para mostrar en la lista desplegable
            var privadas = _context.Privadas.ToList();
 
            ViewBag.Privadas = privadas;

            // Retornar la vista con la lista desplegable vacía
            return View();
        }

        [HttpGet]
        public IActionResult GetViviendas(string foreignKey)
        {

            // Realiza una consulta a la base de datos para obtener los valores que coinciden con la clave foránea
            var data = _context.Viviendas.Where(t => t.Privada_Id == foreignKey).Select(t => new {
                Value = t.Id,
                Name = t.Numero
            }).ToList();

            // Devuelve los datos en formato JSON
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetPropArre(string foreignKey) //Obtener propietario y arrendatario
        {

            // Realiza una consulta a la base de datos para obtener los nombres de los propietarios y arrendatarios correspondientes
            var propietarioNombre = _context.Propietarios.FirstOrDefault(p => p.Vivienda_Id == foreignKey)?.Nombre;
            var arrendatarioNombre = _context.Arrendatarios.FirstOrDefault(a => a.Vivienda_Id == foreignKey)?.Nombre;


            // Devuelve los nombres en formato JSON
            return Json(new { PropietarioNombre = propietarioNombre, ArrendatarioNombre = arrendatarioNombre });
        }

        // POST: Pagos
        [HttpGet]
        public IActionResult ObtenerPagos(string idUsu)
        {
            // Obtener los pagos correspondientes al usuario seleccionado
            var pagos = _context.Pagos.Where(p => p.Vivienda_Id == idUsu).ToList();
            foreach (var pago in pagos)
            {
                pago.Referencia = Convert.ToInt64(pago.Referencia); // Convertir a tipo long
            }

            // Retornar los resultados en formato JSON
            return Json(pagos);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        // GET: Pagos/Create
        public IActionResult Create()
        {
            ViewBag.Viviendas = _context.Viviendas.Select(u => new SelectListItem { Text = u.Numero, Value = u.Id }).ToList();
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public async Task<IActionResult> Create(Pagos pagos)
        {

            if (ModelState.IsValid)
            {

                DateTime fechaprogramada = DateTime.ParseExact(pagos.FechaLimite, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTimeOffset fechaMx = TimeZoneInfo.ConvertTime(new DateTimeOffset(fechaprogramada), tz);
                pagos.FechaLimite = fechaMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");
                DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
                pagos.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");


                _context.Add(pagos);
                await _context.SaveChangesAsync();

                var archivosPagos = new ArchivosPagos
                {
                    Pagos_Id = pagos.Id
                };

                _context.Add(archivosPagos);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Viviendas = _context.Viviendas.Select(u => new SelectListItem { Text = u.Numero, Value = u.Id }).ToList();
            return View(pagos);
        }

        [HttpGet]
        public IActionResult ObtenerArchivo(int id)
        {
            // Buscar el registro de ArchivosLecturas que tenga el mismo valor en la columna Lectura_Id
            var archivo = _context.ArchivosPagos.FirstOrDefault(m => m.Pagos_Id == id);

            // Devolver la información del archivo como resultado de la acción
            return Json(archivo);
        }

        [HttpPost]
        public ActionResult ActualizarArchivo(int id, string accion)
        {
            // Obtener el objeto Pago con el id proporcionado
            var pago = _context.Pagos.Find(id);

            // Asignar el valor a la columna Estado dependiendo del string accion
            if (accion == "Aceptar")
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
                pago.FechaRegistroPago = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

                pago.Estado = "Aprobado";
            }
            else if (accion == "Rechazar")
            {
                pago.Estado = "Rechazado";
                pago.FechaRegistroPago = "--";
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            // Devolver una respuesta al cliente
            return Json(new { success = true });
        }

    }
}
