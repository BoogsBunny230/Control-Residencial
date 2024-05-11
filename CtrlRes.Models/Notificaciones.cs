using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Notificaciones
    {
        public Notificaciones()
        {
            FechaRegistro = DateTime.Now.ToString();
            FechaProgramada = "--";
            Tipo = "Notificación";
            Confirmacion = "--";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Para {get; set; }
        public string FechaRegistro { get; set; }
        public string? Tipo { get; set; }
        public string? FechaProgramada { get; set; }
        public string? Confirmacion {get; set; }

        public string Vivienda_Id { get; set; }

        [ForeignKey("Vivienda_Id")]
        public Viviendas? Viviendas { get; set; }

    }
}
