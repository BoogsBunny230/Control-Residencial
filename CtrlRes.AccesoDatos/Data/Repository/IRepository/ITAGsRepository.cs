using CtrlRes.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.AccesoDatos.Data.Repository.IRepository
{
    public interface ITAGsRepository : IRepository<TAGs>
    {
        IEnumerable<SelectListItem> GetListaTAGs();

        void Update(TAGs tAGs);
    }
}
