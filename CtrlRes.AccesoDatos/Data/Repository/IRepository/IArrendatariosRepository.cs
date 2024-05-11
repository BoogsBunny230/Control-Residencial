using CtrlRes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CtrlRes.AccesoDatos.Data.Repository.IRepository
{
    public interface IArrendatariosRepository : IRepository<Arrendatarios>
    {
        IEnumerable<SelectListItem> GetListaArrendatarios();

        void Update(Arrendatarios arrendatarios);
    }
}
