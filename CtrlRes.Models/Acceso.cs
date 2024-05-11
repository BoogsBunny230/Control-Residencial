using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Visitas
    {

        public Visitas()
        {
            FechaRegistro = DateTime.Now.ToString();
        }

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Numero { get; set; }
        public int? NumeroVisitas { get; set; }
        public string Placas { get; set;}
        public string Datos { get; set; }
        public string Motivo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string FechaRegistro { get; set; }
        public string QR { get; set; }
        public string Emisor { get; set; }
    }
}
