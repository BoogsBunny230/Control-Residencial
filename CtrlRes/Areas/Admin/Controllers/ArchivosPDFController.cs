using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using Microsoft.AspNetCore.Mvc;
using CtrlRes.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ArchivosPDFController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public ArchivosPDFController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]
		public IActionResult Index()
		{
			var archivosPDF = new List<ArchivosPDF>();
			using (SqlConnection con = new SqlConnection("Data Source=SQL5063.site4now.net;Initial Catalog=db_a94235_ctrlres;User Id=db_a94235_ctrlres_admin;Password=SIAUMex2023&"))
			{
				con.Open();
				using (SqlCommand cmd = new("sp_obtener_pdfs", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					var reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						archivosPDF.Add(new ArchivosPDF
						{
							Id = (int)reader["Id"],
							Nombre = reader["Nombre"].ToString(),
							Archivo = reader["Archivo"].ToString()
						});
					}
				}
			}

			// Agregar el código para asignar los valores a las propiedades ViewBag
			ViewBag.Privadas = _context.Privadas.ToList();
			ViewBag.ArchivosPDF = archivosPDF;

			return View(archivosPDF);
		}

		public ActionResult MostrarPDF(string url)
        {
            WebClient client = new WebClient();
            byte[] buffer = client.DownloadData(url);

            return File(buffer, "application/pdf");
        }


		[HttpPost]
		public IActionResult Create(ArchivosPDF archivosPDF)
		{
			if (ModelState.IsValid)
			{
				// Guardar los datos en la base de datos
				_context.ArchivosPDF.Add(archivosPDF);
				_context.SaveChanges();

				// Devolver los datos actualizados en formato JSON
				var archivosPDFs = _context.ArchivosPDF.ToList();
				return Json(archivosPDFs);
			}
			return BadRequest();
		}


		[HttpGet("[action]")]
		public IActionResult Create()
		{
			return View();
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult Create(IFormFile file)
		{
			using (var memorystream = new MemoryStream())
			{
				file.CopyTo(memorystream);
				var fileBytes = memorystream.ToArray();
				var fileData = Convert.ToBase64String(fileBytes);

				using (SqlConnection con = new SqlConnection("Data Source=SQL5063.site4now.net;Initial Catalog=db_a94235_ctrlres;User Id=db_a94235_ctrlres_admin;Password=SIAUMex2023&"))
				{
					con.Open();
					using (SqlCommand cmd = new("sp_insertar_pdf", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = file.FileName;
						cmd.Parameters.Add("@Archivo", SqlDbType.VarChar).Value = fileData;
						cmd.ExecuteNonQuery();
					}
				}
			}
			return RedirectToAction("Index");
		}

		public ArchivosPDF? ObtenerPdf(int id)
		{
			using (SqlConnection con = new SqlConnection("Data Source=SQL5063.site4now.net;Initial Catalog=db_a94235_ctrlres;User Id=db_a94235_ctrlres_admin;Password=SIAUMex2023&"))
			{
				con.Open();
				using (SqlCommand cmd = new("sp_buscar_pdf", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

					var reader = cmd.ExecuteReader();

					if (reader.Read())
					{
						var fileName = reader.GetString(1);
						var fileData = reader.GetString(2);

						ArchivosPDF pdf = new();
						pdf.Id = id;
						pdf.Nombre = fileName;
						pdf.Archivo = fileData;
						return pdf;
					}
					else
					{
						return null;
					}
				}
			}
		}

		[HttpGet]
		public IActionResult Descargar(int id)
		{
			ArchivosPDF? pdf = ObtenerPdf(id);
			if (pdf != null && pdf.Archivo != null)
			{
				var fileBytes = Convert.FromBase64String(pdf.Archivo);
				var memorystream = new MemoryStream(fileBytes);
				return File(memorystream, "application/pdf", pdf.Nombre);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet]
		public IActionResult ObtenerBase64String(int id)
		{
			// Obtener el archivo de la base de datos por su ID
			ArchivosPDF? pdf = ObtenerPdf(id);

			if (pdf != null && pdf.Archivo != null)
			{
				// Devolver la cadena en base64 como resultado
				return Content(pdf.Archivo);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet]
		public ActionResult ObtenerPDFDesdeBD(int id)
		{
			ArchivosPDF? pdf = ObtenerPdf(id);

			
			if (pdf != null && pdf.Archivo != null)
			{
				// Devolver la cadena en base64 como resultado
				byte[] pdfBytes = Convert.FromBase64String(pdf.Archivo);
				return File(pdfBytes, "application/pdf");
			}
			else
			{
				return NotFound();
			}
			
		}

		[HttpPost]
		public ActionResult Asignar(string IdPriv, int Id)
		{
			// Obtener el registro de Privadas correspondiente al IdPriv seleccionado
			var privada = _context.Privadas.Find(IdPriv);

			// Asignar la clave externa
			privada.ArchivosPDFId = Id;

			// Guardar los cambios en la base de datos
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult AsignarCodigoUrbano(string IdPriv, int Id)
		{
			// Obtener el registro de Privadas correspondiente al IdPriv seleccionado
			var privada = _context.Privadas.Find(IdPriv);

			// Asignar la clave externa
			privada.ArchivosPDFIdCU = Id;

			// Guardar los cambios en la base de datos
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}
