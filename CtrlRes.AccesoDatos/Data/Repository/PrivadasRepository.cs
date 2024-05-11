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
    public class PrivadasRepository : Repository<Privadas>, IPrivadasRepository
    {
        private readonly ApplicationDbContext _db;

        public PrivadasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaPrivadas()
        {
            return _db.Privadas.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.IdPriv.ToString()
            }
            );
        }

        public void Update(Privadas privadas)
        {
            var ObjDesdeDb = _db.Privadas.FirstOrDefault(s => s.IdPriv == privadas.IdPriv);
            ObjDesdeDb.Nombre = privadas.Nombre;
            ObjDesdeDb.MedLuz = privadas.MedLuz;
            ObjDesdeDb.Observaciones = privadas.Observaciones;

            _db.SaveChanges();
        }
    }
}
