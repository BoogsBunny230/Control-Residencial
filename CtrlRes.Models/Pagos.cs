using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Pagos
    {

        public Pagos()
        {
            FechaRegistro = DateTime.Now.ToString();
            Estado = "Sin Pagar";
            FechaPago = "--";
            FechaRegistroPago = "--";
            Para = "Ambos";
            Concepto = "Concepto";
            Vivienda_Id = "";
        }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        public long Referencia { get; set; }
        public string Concepto { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaLimite { get; set; }
        public string? FechaPago { get; set; }
        public string? FechaRegistroPago { get; set; }
        public string Para { get; set; }

        public string Vivienda_Id { get; set; }

        [ForeignKey("Vivienda_Id")]
        public Viviendas? Viviendas { get; set; }

    }

    public class ArchivosPagos
    {
        public ArchivosPagos() {
            Archivo = "";
            FechaSubida = null;     
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Archivo { get; set; }
        public string? FechaSubida { get; set; }
        public int? Pagos_Id { get; set; }

        [ForeignKey("Pagos_Id")]
        public Pagos? Pagos { get; set; }
    }


}
