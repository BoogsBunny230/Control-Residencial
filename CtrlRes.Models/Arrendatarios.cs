using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Arrendatarios
    {
        public Arrendatarios()
        {
            Id = "Arrendatario";
            Inicio_Arrendatario = DateTime.Now.ToString();
            Fin_Arrendatario = null;
        }

        [Key]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Vehiculo { get; set; }

        public string? Inicio_Arrendatario{ get; set; }
        public string? Fin_Arrendatario { get; set; }

        public string Vivienda_Id { get; set; }

        [ForeignKey("Vivienda_Id")]
        public Viviendas? Viviendas { get; set; }
    }
}
