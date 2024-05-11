using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Egresos
    {
        public Egresos() {

            FechaRegistro = "--";
        
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Tipo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public string Motivo { get; set; }
        public string FechaRegistro { get; set; }

        public string Privada_Id { get; set; }

        [ForeignKey("Privada_Id")]
        public Privadas? Privadas { get; set; }
    }
}
