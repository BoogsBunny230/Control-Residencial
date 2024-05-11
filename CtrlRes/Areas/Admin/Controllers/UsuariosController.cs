using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using CtrlRes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewBag.ApplicationUser = _context.ApplicationUser.ToList();
            ViewBag.RoleNames = new List<string>();
            foreach (var user in ViewBag.ApplicationUser)
            {
                var applicationUser = (ApplicationUser)user;
                var roleId = _context.UserRoles.FirstOrDefault(r => r.UserId == applicationUser.Id).RoleId;
                var roleName = _context.Roles.FirstOrDefault(r => r.Id == roleId).Name;
                ViewBag.RoleNames.Add(roleName);
            }
            return View();
        }

        //Borrar
        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.DeleteAsync(user);
                return Json(new { success = true, message = "Registro eliminado correctamente" });
            }
            return Json(new { success = false, message = "Error al borrar" });
        }

        [Authorize(Roles = "Administrador, Creador/Editor, Creador")]
        public IActionResult Create()
        {
            // Código de la acción

            return RedirectToAction("Register", "Account", new { area = "Identity" });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(string id, string[] roles, ApplicationUser user)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User != null)
            {
                // Actualizar roles
                var currentRoles = await _userManager.GetRolesAsync(User);
                await _userManager.RemoveFromRolesAsync(User, currentRoles);
                await _userManager.AddToRolesAsync(User, roles);

                // Actualizar otras propiedades
                User.Nombre = user.Nombre;
                User.UserName = user.UserName;
                // Otras propiedades

                // Guardar cambios
                await _userManager.UpdateAsync(User);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Creador/Editor, Editor")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Obtener roles del usuario
            var roles = await _userManager.GetRolesAsync(user);

            // Obtener lista de roles disponibles
            var availableRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            // Crear modelo de vista
            var model = new EditUserViewModel
            {
                User = user,
                Roles = roles,
                AvailableRoles = availableRoles
            };

            return View(model);
        }
    }
}