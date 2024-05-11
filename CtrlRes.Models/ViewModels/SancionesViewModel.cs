using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models.ViewModels
{
    public class SancionesViewModel
    {
        public Sanciones Sanciones { get; set; }
        public SancionesConceptos SancionesConceptos { get; set; }
        public Pagos Pagos { get; set; }
        public IFormFile Archivo { get; set; }
        public string GuardarConcepto { get; set; }
    }
}
