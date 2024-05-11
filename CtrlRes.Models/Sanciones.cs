using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Sanciones
    {

        public Sanciones()
        {
            NombreArchivo = "Nombre Archivo";
            Archivo = "";
            Notificacion = "Notificacion";
            FechaRegistro = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }
        public string Mensaje { get; set; }
        public string? FechaIncidente { get; set; }
        public string? FechaRegistro { get; set; }
        public string NombreArchivo { get; set; }
        public string? Archivo { get; set; }
        public string Notificacion { get; set; }
        //public string? Para { get; set; }

        public string Vivienda_Id { get; set; }

        [ForeignKey("Vivienda_Id")]
        public Viviendas? Viviendas { get; set; }
    }

    public class SancionesConceptos
    {
        public SancionesConceptos()
        {
            Concepto = "Concepto";
            Monto = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }
    }
}
