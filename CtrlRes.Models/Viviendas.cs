using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Viviendas
    {
        public Viviendas() {

            Id = "Vivienda";

        }

        public string Id { get; set; }
        public string Numero { get; set; }
        public string MedidorAgua { get; set; }

        public string Privada_Id { get; set; }

        [ForeignKey("Privada_Id")]
        public Privadas? Privadas { get; set; } // propiedad de navegación

    }
}
