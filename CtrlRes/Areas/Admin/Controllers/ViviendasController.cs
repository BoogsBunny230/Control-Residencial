using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Data.Migrations;
using CtrlRes.Models;
using CtrlRes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ViviendasController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ViviendasController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var arrendatarios = _context.Arrendatarios.ToList();
            var propietarios = _context.Propietarios.ToList();
            var viviendas = _context.Viviendas.ToList();

            ViewBag.Arrendatarios = arrendatarios;
            ViewBag.Propietarios = propietarios;
            ViewBag.Viviendas = viviendas;

            return View();
        }

        //Agregar nueva tarjeta Catalogo
        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            ViewBag.Privadas = _context.Privadas.ToList();

            var viviendas = new Viviendas();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ViviendasCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Accede a los modelos y guárdalos en tu base de datos
                var viviendas = viewModel.Viviendas;
                var propietarios = viewModel.Propietarios;
                var arrendatarios = viewModel.Arrendatarios;


                //Asignación de Id para propietario y arrendatario

                propietarios.Id = propietarios.Vivienda_Id + "_Prop";
                arrendatarios.Id = arrendatarios.Vivienda_Id + "_Arren";
                viviendas.Id = "Vivienda" + viviendas.Numero;

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
                propietarios.Inicio_Propietario = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");
                arrendatarios.Inicio_Arrendatario = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

                _context.Viviendas.Add(viviendas);
                _context.Propietarios.Add(propietarios);
                _context.Arrendatarios.Add(arrendatarios);
                _context.SaveChanges();


                //Inicia creación usuario propietario
                var userName = "P" + viviendas.Numero + propietarios.Inicio_Propietario.Substring(8, 2) + propietarios.Inicio_Propietario.Substring(5, 2) + propietarios.Inicio_Propietario.Substring(2, 2);
                var password = "Pp" + viviendas.Numero + propietarios.Inicio_Propietario.Substring(8, 2) + propietarios.Inicio_Propietario.Substring(5, 2) + propietarios.Inicio_Propietario.Substring(2, 2) + "&";

                var user = CreateUser();

                user.UserName = userName;
                user.Nombre = propietarios.Nombre;
                user.Contrasena = password;
                user.UsuarioTipo = "Propietario";
                user.PropOrArr_Id = propietarios.Id;
                user.Estado = "Activo";

                var result = await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, "Propietario");

                //Inicia creación usuario arrendatario

                var userName2 = "A" + viviendas.Numero + arrendatarios.Inicio_Arrendatario.Substring(8, 2) + arrendatarios.Inicio_Arrendatario.Substring(5, 2) + arrendatarios.Inicio_Arrendatario.Substring(2, 2);
                var password2 = "Aa" + viviendas.Numero + arrendatarios.Inicio_Arrendatario.Substring(8, 2) + arrendatarios.Inicio_Arrendatario.Substring(5, 2) + arrendatarios.Inicio_Arrendatario.Substring(2, 2) + "&";

                var user2 = CreateUser();

                user2.UserName = userName2;
                user2.Nombre = arrendatarios.Nombre;
                user2.Contrasena = password2;
                user2.UsuarioTipo = "Arrendatario";
                user2.PropOrArr_Id = arrendatarios.Id;
                user2.Estado = "Activo";

                if (arrendatarios.Nombre != "--" && arrendatarios.Celular != "--" && arrendatarios.Vehiculo != "--")
                {
                    var result2 = await _userManager.CreateAsync(user2, password2);
                    await _userManager.AddToRoleAsync(user2, "Arrendatario");
                }

                //Termina

                ViewBag.Privadas = _context.Privadas.ToList();
                return Json(new { User1 = user, User2 = user2 , Vivienda = viviendas});
                //return RedirectToAction("Index", "Propietarios", new { area = "Admin" });
            }

            ViewBag.Privadas = _context.Privadas.ToList();
            return View(viewModel);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        [HttpPost]
        public JsonResult CheckNumero(string numero)
        {
            bool exists = _context.Viviendas.Any(v => v.Numero == numero);
            return Json(new { exists = exists });
        }

    }
}
