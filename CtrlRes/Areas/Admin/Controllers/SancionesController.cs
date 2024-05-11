using CtrlRes.Data;
using CtrlRes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using CtrlRes.Models;


namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SancionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SancionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Obtener el listado de privadas para mostrar en la lista desplegable
            var privadas = _context.Privadas.ToList();
            var sancionesConceptos = _context.SancionesConceptos.ToList();

            ViewBag.Privadas = privadas;
            ViewBag.SancionesConceptos = sancionesConceptos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(SancionesViewModel viewModel)
        {
            var sancionesConceptos = viewModel.SancionesConceptos;
            var sanciones = viewModel.Sanciones;
            var pagos = viewModel.Pagos;

            // Obtener el objeto "Pagos" y asignar valores faltantes

            pagos.Estado = "Sin Pagar";

            //

            // Convierte la fecha en formato de cadena a un objeto DateTime
            DateTime fechalimite = DateTime.ParseExact(pagos.FechaLimite, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            DateTime fechaincidente = DateTime.ParseExact(sanciones.FechaIncidente, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            // Crea un objeto TimeZoneInfo que representa la zona horaria de México
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");

            // Crea un objeto DateTimeOffset que incluye la fecha y la zona horaria de México
            DateTimeOffset fechaMx = TimeZoneInfo.ConvertTime(new DateTimeOffset(fechalimite), tz);
            DateTimeOffset fechaMx2 = TimeZoneInfo.ConvertTime(new DateTimeOffset(fechaincidente), tz);

            // Crea una cadena de texto en formato ISO 8601 que incluye la fecha y hora en la zona horaria de México
            pagos.FechaLimite = fechaMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");
            sanciones.FechaIncidente = fechaMx2.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            // Crea un objeto DateTimeOffset que incluye la fecha y hora actual en la zona horaria de México
            DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);

            // Convierte el objeto DateTimeOffset de vuelta a una cadena en formato ISO 8601
            pagos.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");
            sanciones.FechaRegistro = pagos.FechaRegistro;

            pagos.FechaRegistroPago = null;
            pagos.Vivienda_Id = sanciones.Vivienda_Id;
            pagos.Para = "Ambos";
            pagos.Concepto = sanciones.Concepto;
            pagos.Monto = (decimal)sanciones.Monto;

            // Accede a los modelos 

            sanciones.Archivo = "";

            if (ModelState.IsValid)
            {

                using (var memorystream = new MemoryStream())
                {
                    viewModel.Archivo.CopyTo(memorystream);
                    var fileBytes = memorystream.ToArray();
                    var fileData = Convert.ToBase64String(fileBytes);

                    sanciones.Archivo = fileData;
                    sanciones.NombreArchivo = viewModel.Archivo.FileName;
                    _context.Sanciones.Add(sanciones);

                }

                if (viewModel.GuardarConcepto == "Si")
                {
                    _context.SancionesConceptos.Add(sancionesConceptos);
                }
                _context.Pagos.Add(pagos);
                _context.SaveChanges();

                Notificaciones notificaciones = new Notificaciones
                {
                    Titulo = sanciones.Concepto,
                    Mensaje = sanciones.Mensaje,
                    Para = "Ambos",
                    FechaRegistro = sanciones.FechaRegistro,
                    Tipo = "Sancion",
                    Vivienda_Id = sanciones.Vivienda_Id
                };

                var result = await new NotificacionesController(_context).ForeingPostAsync(notificaciones, "Sanciones");
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
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

        [HttpGet]
        public IActionResult GetSanConcepto(int foreignKey) //Obtener propietario y arrendatario
        {

            // Realiza una consulta a la base de datos para obtener los nombres de los propietarios y arrendatarios correspondientes
            var sancionConcepto = _context.SancionesConceptos.FirstOrDefault(p => p.Id == foreignKey)?.Concepto;
            var sancionMonto = _context.SancionesConceptos.FirstOrDefault(a => a.Id == foreignKey)?.Monto;


            // Devuelve los nombres en formato JSON
            return Json(new { SancionConcepto = sancionConcepto, SancionMonto = sancionMonto });
        }
    }
}
