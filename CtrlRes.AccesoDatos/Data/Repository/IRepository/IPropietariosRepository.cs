using CtrlRes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CtrlRes.AccesoDatos.Data.Repository.IRepository
{
    public interface IPropietariosRepository : IRepository<Propietarios>
    {
        IEnumerable<SelectListItem> GetListaPropietarios();

        void Update(Propietarios propietarios);
    }
}
