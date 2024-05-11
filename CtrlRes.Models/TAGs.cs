using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class TAGs
    {
        [Key]
        public string TAG { get; set; }
        public string Propietario { get; set; }
        public string Privada { get; set; }
    }
}
