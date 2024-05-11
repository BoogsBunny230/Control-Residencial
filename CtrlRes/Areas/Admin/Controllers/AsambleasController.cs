using CtrlRes.Data;
using CtrlRes.Models.ViewModels;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AsambleasController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AsambleasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Obtener el listado de privadas para mostrar en la lista desplegable
            var privadas = _context.Privadas.ToList();

            ViewBag.Privadas = privadas;

            return View();
        }

        [HttpGet]
        public IActionResult GetViviendas(string selectedValue)
        {

            // Realiza una consulta a la base de datos para obtener los valores que coinciden con la clave foránea
            var data = _context.Viviendas.Where(t => t.Privada_Id == selectedValue).Select(t => new {
                Value = t.Id,
                Name = t.Numero
            }).ToList();

            // Devuelve los datos en formato JSON
            return Json(data);
        }

        //POST *************************************************************************************************************************************

        [HttpPost]
        public async Task<IActionResult> IndexAsync(Asambleas asambleas)
        {

            // Convierte la fecha en formato de cadena a un objeto DateTime
            DateTime fechaprogramada = DateTime.ParseExact(asambleas.FechaProgramada, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            // Crea un objeto TimeZoneInfo que representa la zona horaria de México
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");

            // Crea un objeto DateTimeOffset que incluye la fecha y la zona horaria de México
            DateTimeOffset fechaMx = TimeZoneInfo.ConvertTime(new DateTimeOffset(fechaprogramada), tz);

            // Crea una cadena de texto en formato ISO 8601 que incluye la fecha y hora en la zona horaria de México
            asambleas.FechaProgramada = fechaMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Crea un objeto DateTimeOffset que incluye la fecha y hora actual en la zona horaria de México
            DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);

            // Convierte el objeto DateTimeOffset de vuelta a una cadena en formato ISO 8601
            asambleas.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Accede a los modelos 

            if (ModelState.IsValid)
            {


                _context.Asambleas.Add(asambleas);


                _context.SaveChanges();

                string[] valores = asambleas.Notificacion.Split(',');

                foreach (string valor in valores)
                {
                    Notificaciones notificaciones = new Notificaciones
                    {
                        Titulo = asambleas.Nombre,
                        Mensaje = asambleas.Descripcion,
                        Para = "Ambos",
                        FechaRegistro = asambleas.FechaRegistro,
                        FechaProgramada = asambleas.FechaProgramada,
                        Tipo = "Asamblea",
                        Vivienda_Id = valor
                    };

                    var result = await new NotificacionesController(_context).ForeingPostAsync(notificaciones, "Asambleas");
                }

                ViewBag.Privadas = _context.Privadas.ToList();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Privadas = _context.Privadas.ToList();
            return View(asambleas);
        }

        //--------------------------------------------------HISTORIAL-----------------------------------------------------------

        [HttpGet]
        public IActionResult GetHistorial(string selectedValue)
        {

            // Realiza una consulta a la base de datos para obtener los valores que coinciden con la clave foránea
            var data = _context.Asambleas.Where(t => t.Privada_Id == selectedValue).Select(t => new {
                Value = t.Id,
                Name = t.Nombre,
                Fecha = t.FechaProgramada
            }).ToList();

            // Devuelve los datos en formato JSON
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticsAsync(int id)
        {
            var asamblea = await _context.Asambleas.FindAsync(id);

            if (asamblea == null)
            {
                return NotFound();
            }

            var notificados = await _context.Notificaciones.Where(u => u.FechaRegistro == asamblea.FechaRegistro).ToListAsync();

            var estadisticas = new
            {
                totalNotificados = notificados.Count,
                confirmados = notificados.Count(n => n.Confirmacion == "Si"),
                sinconfirmar = notificados.Count(n => n.Confirmacion == "--"),
                denegados = notificados.Count(n => n.Confirmacion == "No"),
                descripcionAsamblea = asamblea.Descripcion
            };

            return Json(estadisticas);
        }

    }
}
