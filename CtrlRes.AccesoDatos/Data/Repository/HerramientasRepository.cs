using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CtrlRes.AccesoDatos.Data.Repository
{
    public class HerramientasRepository : Repository<Herramientas>, IHerramientasRepository
    {
        private readonly ApplicationDbContext _db;

        public HerramientasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaHerramientas()
        {
            return _db.Herramientas.Select(i => new SelectListItem()
            {
                Text = i.IdHerr,
                Value = i.IdHerr.ToString()
            }
            );
        }

        public void Update(Herramientas herramientas)
        {
            var ObjDesdeDb = _db.Herramientas.FirstOrDefault(s => s.IdHerr == herramientas.IdHerr);
            ObjDesdeDb.Contenido = herramientas.Contenido;

            _db.SaveChanges();
        }
    }
}
