using CtrlRes.Data;
using CtrlRes.Data.Migrations;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LecturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecturas
        public IActionResult Index()
        {
            // Obtener el listado de usuarios para mostrar en la lista desplegable
            var privadas = _context.Privadas.ToList();
            ViewBag.Privadas = privadas;

            // Retornar la vista con la lista desplegable vacía
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerLecturas(string privada)
        {
            // Obtener los lecturas correspondientes al usuario seleccionado
            var lecturas = _context.RegistroLecturas.Where(p => p.Privada_Id == privada).ToList();

            // Por cada objeto de la lista lecturas
            foreach (var lectura in lecturas)
            {
                // Buscar en la base de datos del modelo Lectuas, los elementos que sean concidentes en la columna RegistroLecturas_Id con el valor de lecturas.Folio
                var lecturasRealizadas = _context.Lecturas.Where(l => l.RegistroLecturas_Id == lectura.Folio).ToList();

                // Calcular cuantos de los elementos encontrados tienen en la columna "Estado" un valor de "Realizado"
                int realizadas = lecturasRealizadas.Count(l => l.Estado == "Realizada");

                // Calcular el porcentaje que representa la cantidad de realizadas
                double porcentaje = (double)realizadas / lecturasRealizadas.Count * 100;

                // Asignar el resultado a lecturas.Estado de la lista de objetos en un formato tipo "5 de 20 (25%)"
                lectura.Estado = $"{realizadas} de {lecturasRealizadas.Count} ({porcentaje}%)";
            }

            // Retornar los resultados en formato JSON
            return Json(lecturas);
        }


        [HttpGet]
        // GET: Lecturas/Create
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            ViewBag.Privadas = _context.Privadas.Select(u => new SelectListItem { Text = u.Nombre, Value = u.IdPriv }).ToList();
            return View();
        }

        // POST: Lecturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public async Task<IActionResult> Create([Bind("Folio,TipLec,FechaProgramada,Privada_Id")] RegistroLecturas registroLecturas)
        {

            // Convierte la fecha en formato de cadena a un objeto DateTime
            DateTime fechaprogramada = DateTime.ParseExact(registroLecturas.FechaProgramada, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            // Crea un objeto TimeZoneInfo que representa la zona horaria de México
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");

            // Crea un objeto DateTimeOffset que incluye la fecha y la zona horaria de México
            DateTimeOffset fechaMx = TimeZoneInfo.ConvertTime(new DateTimeOffset(fechaprogramada), tz);

            // Crea una cadena de texto en formato ISO 8601 que incluye la fecha y hora en la zona horaria de México
            registroLecturas.FechaProgramada = fechaMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Crea un objeto DateTimeOffset que incluye la fecha y hora actual en la zona horaria de México
            DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);

            // Convierte el objeto DateTimeOffset de vuelta a una cadena en formato ISO 8601
            registroLecturas.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Accede a los modelos

            if (ModelState.IsValid)
            {

                _context.Add(registroLecturas);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Privadas = _context.Privadas.Select(u => new SelectListItem {Text = u.Nombre, Value = u.IdPriv }).ToList();
            return View(registroLecturas);
        }

        [HttpPost]
        public JsonResult CheckFolio(string folio)
        {
            bool exists = _context.RegistroLecturas.Any(v => v.Folio == folio);
            return Json(new { exists = exists });
        }

        [HttpGet]
        public IActionResult ObtenerLecturasChildren(string folio)
        {
            // Obtener los lecturas correspondientes al usuario seleccionado
            var lecturasChildren = _context.Lecturas.Where(p => p.RegistroLecturas_Id == folio).ToList();

            // Retornar los resultados en formato JSON
            return Json(lecturasChildren);
        }

        [HttpGet]
        public IActionResult ObtenerArchivo(int id)
        {
            // Buscar el registro de ArchivosLecturas que tenga el mismo valor en la columna Lectura_Id
            var archivo = _context.ArchivoLecturas.FirstOrDefault(m => m.Lectura_Id == id);

            // Devolver la información del archivo como resultado de la acción
            return Json(archivo);
        }

        [HttpPost]
        public ActionResult ActualizarArchivo(int id, string accion, decimal coreccion)
        {
            // Obtener el objeto Pago con el id proporcionado
            var lectura = _context.Lecturas.Find(id);

            // Asignar el valor a la columna Estado dependiendo del string accion
            if (accion == "Aceptar")
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
                lectura.FechaAprobacion = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

                lectura.Estado = "Aprobado";
            }
            else if (accion == "Rechazar")
            {
                lectura.Estado = "Rechazado";
                lectura.FechaAprobacion = "--";
            }
            else if (accion == "Corregir")
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
                lectura.FechaAprobacion = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

                lectura.Estado = "Aprobado";
                lectura.ValorLectura = coreccion;
                lectura.Correccion = true;
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            // Devolver una respuesta al cliente
            return Json(new { success = true });
        }
    }
}