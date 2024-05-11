using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models.ViewModels
{
    public class MantenimientosViewModel
    {
        public Mantenimientos Mantenimientos { get; set; }
        public MantenimientosConceptos MantenimientosConceptos { get; set; }
        public string GuardarConcepto { get; set; }
    }
}
