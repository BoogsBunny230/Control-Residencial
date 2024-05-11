using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models
{
    public class Mediciones
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string? Fotografia { get; set; }
        public string? Medicion {get; set; }

        [Required]
        public string Folio { get; set; }
      
        [ForeignKey("Folio")]
        public Lecturas Lecturas { get; set; } // propiedad de navegación

    }
}
