using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CtrlRes.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {

            PropOrArr_Id = null;
        }

        public string? Nombre { get; set; }
        public string? Contrasena { get; set; }
        public string? UsuarioTipo { get; set; }
        public string? PropOrArr_Id { get; set; }
        public string? Estado { get; set; }
        public string? PushToken { get; set; }
    }
}