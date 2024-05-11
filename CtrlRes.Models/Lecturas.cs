using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class RegistroLecturas
    {
        public RegistroLecturas()
        {
            Estado = "Pendiente";
            FechaRegistro = "";

        }


        [Key]
        public string Folio { get; set; }
        public string TipLec { get; set; }
        public string Estado { get; set; }
        public string FechaProgramada { get; set; }
        public string FechaRegistro { get; set; }

        public string Privada_Id { get; set; }

        [ForeignKey("Privada_Id")]
        public Privadas? Privadas { get; set; }
    }

    public class Lecturas
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TipLec { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal ValorLectura { get; set; }
        public string Estado { get; set; }
        public string NivelRetraso { get; set; }
        public bool Correccion { get; set; }
        public string FechaRealizacion { get; set; }
        public string FechaAprobacion { get; set; }
        public string RegistroLecturas_Id { get; set; }
        public string Vivienda_Id { get; set; }


        [ForeignKey("RegistroLecturas_Id")]
        public RegistroLecturas? RegistroLecturas { get; set; }
    }

    public class ArchivosLecturas
    {
        public ArchivosLecturas()
        {
            Archivo = "";
            FechaSubida = null;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Archivo { get; set; }
        public string? FechaSubida { get; set; }
        public int? Lectura_Id { get; set; }

        [ForeignKey("Lectura_Id")]
        public Lecturas? Lecturas { get; set; }
    }
}
