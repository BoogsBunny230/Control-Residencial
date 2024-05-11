using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Privadas
    {
		public Privadas()
		{
			ArchivosPDFId = 1;
			ArchivosPDFIdCU = 1;
		}

		[Key]
        public string IdPriv { get; set; }
        public string Nombre { get; set; }
        public string MedLuz { get; set; }
        public string Observaciones { get; set; }

		public int? ArchivosPDFId { get; set; } // clave externa nullable
		public int? ArchivosPDFIdCU { get; set; } // clave externa nullable

		[ForeignKey("ArchivosPDFId")]
		public ArchivosPDF? ArchivosPDF { get; set; } // propiedad de navegación

		[ForeignKey("ArchivosPDFIdCU")]
		public ArchivosPDF? ArchivosPDFCU { get; set; } // propiedad de navegación



	}
}
