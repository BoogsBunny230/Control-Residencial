using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Proveedores
    {
        [Key]
        public string IdProv { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public string Servicios { get; set; }
    }
}
