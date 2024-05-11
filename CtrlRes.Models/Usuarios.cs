using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Usuarios
    {
        [Key]
        [Required]
        public string IdUsu { get; set; }
        [Required]
        public string UserName { get; set;}
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
