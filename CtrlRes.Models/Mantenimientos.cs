using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Mantenimientos
    {
        public Mantenimientos() {

            FechaRegistro = "";
        
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Folio { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public string Mensaje { get; set; }
        public string FechaProgramada { get; set; }
        public string FechaRegistro { get; set; }
        public string Notificacion { get; set; }

        public string Privada_Id { get; set; }

        [ForeignKey("Privada_Id")]
        public Privadas? Privadas { get; set; } // propiedad de navegación
    }

    public class MantenimientosConceptos
    {

        public MantenimientosConceptos ()
        {
            Concepto = "Concepto";
            Descripcion = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
    }

}
