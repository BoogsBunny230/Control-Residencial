using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.Models.ViewModels
{
    public class PropietariosVM
    {
        public Propietarios Propietarios { get; set; }
        public IEnumerable<SelectListItem> ListaPrivadas { get; set; }
    }
}
