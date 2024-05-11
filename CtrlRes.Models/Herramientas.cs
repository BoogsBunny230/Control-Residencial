using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Herramientas
    {
        [Key]
        public string IdHerr { get; set; }
        public string Contenido { get; set; }
    }
}
