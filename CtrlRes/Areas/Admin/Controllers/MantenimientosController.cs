using CtrlRes.Data;
using CtrlRes.Models.ViewModels;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MantenimientosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MantenimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Obtener el listado de privadas para mostrar en la lista desplegable
            var privadas = _context.Privadas.ToList();
            var mantenimientosConceptos = _context.MantenimientosConceptos.ToList();

            ViewBag.Privadas = privadas;
            ViewBag.MantenimientosConceptos = mantenimientosConceptos;

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

        [HttpGet]
        public IActionResult GetManConcepto(int foreignKey) //Obtener propietario y arrendatario
        {

            // Realiza una consulta a la base de datos para obtener los nombres de los propietarios y arrendatarios correspondientes
            var mantenimientoConcepto = _context.MantenimientosConceptos.FirstOrDefault(p => p.Id == foreignKey)?.Concepto;
            var mantenimientoDescripcion = _context.MantenimientosConceptos.FirstOrDefault(a => a.Id == foreignKey)?.Descripcion;


            // Devuelve los nombres en formato JSON
            return Json(new { MantenimientoConcepto = mantenimientoConcepto, MantenimientoDescripcion = mantenimientoDescripcion });
        }

        //POST *************************************************************************************************************************************

        [HttpPost]
        public async Task<IActionResult> IndexAsync(MantenimientosViewModel viewModel)
        {
            var mantenimientosConceptos = viewModel.MantenimientosConceptos;
            var mantenimientos = viewModel.Mantenimientos;
 

            // Convierte la fecha en formato de cadena a un objeto DateTime
            DateTime fechaprogramada = DateTime.ParseExact(mantenimientos.FechaProgramada, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            // Crea un objeto TimeZoneInfo que representa la zona horaria de México
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");

            // Crea un objeto DateTimeOffset que incluye la fecha y la zona horaria de México
            DateTimeOffset fechaMx = TimeZoneInfo.ConvertTime(new DateTimeOffset(fechaprogramada), tz);

            // Crea una cadena de texto en formato ISO 8601 que incluye la fecha y hora en la zona horaria de México
            mantenimientos.FechaProgramada = fechaMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Crea un objeto DateTimeOffset que incluye la fecha y hora actual en la zona horaria de México
            DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);

            // Convierte el objeto DateTimeOffset de vuelta a una cadena en formato ISO 8601
            mantenimientos.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Accede a los modelos 

            if (ModelState.IsValid)
            {


                _context.Mantenimientos.Add(mantenimientos);

                if (viewModel.GuardarConcepto == "Si")
                {
                    _context.MantenimientosConceptos.Add(mantenimientosConceptos);
                }

                _context.SaveChanges();

                string[] valores = mantenimientos.Notificacion.Split(',');

                foreach (string valor in valores)
                {
                    Notificaciones notificaciones = new Notificaciones
                    {
                        Titulo = mantenimientos.Concepto,
                        Mensaje = mantenimientos.Mensaje,
                        Para = "Ambos",
                        FechaRegistro = mantenimientos.FechaRegistro,
                        FechaProgramada = mantenimientos.FechaProgramada,
                        Tipo = "Mantenimiento",
                        Vivienda_Id = valor
                    };

                    var result = await new NotificacionesController(_context).ForeingPostAsync(notificaciones, "Mantenimientos");
                }

                ViewBag.Privadas = _context.Privadas.ToList();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Privadas = _context.Privadas.ToList();
            ViewBag.MantenimientosConceptos = _context.MantenimientosConceptos.ToList();
            return View(viewModel);
        }


    }
}
