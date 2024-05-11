using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Comunidad
    {
        public Comunidad() 
        {
            Visto = false;
            Respondido = false;
            Respuesta = "--";
            Oculto = false;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Asunto {get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public string FechaRegistro { get; set; }
        public bool? Visto { get; set; }
        public bool? Respondido { get; set; } 
        public string? Respuesta { get; set; }
        public bool? Oculto { get; set; }
        public string? FechaRegistroRespuesta { get; set; }

        public string Emisor { get; set; }

        [ForeignKey("Emisor")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
