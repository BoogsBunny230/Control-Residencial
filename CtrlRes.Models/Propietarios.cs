using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Propietarios
    {

        public Propietarios()
        {
            Id = "Propietario";
            Inicio_Propietario = DateTime.Now.ToString();
            Fin_Propietario = null;
        }

        [Key]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Vehiculo { get; set; }

        public string? Inicio_Propietario { get; set; }
        public string? Fin_Propietario { get; set; }

        public string Vivienda_Id { get; set; }

        [ForeignKey("Vivienda_Id")]
        public Viviendas? Viviendas { get; set; }
    }
}
