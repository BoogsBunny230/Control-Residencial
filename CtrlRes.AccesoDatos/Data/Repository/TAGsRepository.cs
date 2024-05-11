using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.AccesoDatos.Data.Repository
{
    public class TAGsRepository : Repository<TAGs>, ITAGsRepository
    {
        private readonly ApplicationDbContext _db;

        public TAGsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaTAGs()
        {
            return _db.TAGs.Select(i => new SelectListItem()
            {
                Text = i.TAG,
                Value = i.TAG.ToString()
            }
            );
        }

        public void Update(TAGs tAGs)
        {
            var ObjDesdeDb = _db.TAGs.FirstOrDefault(s => s.TAG == tAGs.TAG);
            ObjDesdeDb.Propietario = tAGs.Propietario;
            ObjDesdeDb.Privada = tAGs.Privada;

            _db.SaveChanges();
        }
    }
}
