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
    public class ProveedoresRepository : Repository<Proveedores>, IProveedoresRepository
    {
        private readonly ApplicationDbContext _db;

        public ProveedoresRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaProveedores()
        {
            return _db.Proveedores.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.IdProv.ToString()
            }
            );
        }

        public void Update(Proveedores proveedores)
        {
            var ObjDesdeDb = _db.Proveedores.FirstOrDefault(s => s.IdProv == proveedores.IdProv);
            ObjDesdeDb.Nombre = proveedores.Nombre;
            ObjDesdeDb.Numero = proveedores.Numero;
            ObjDesdeDb.Servicios = proveedores.Servicios;

            _db.SaveChanges();
        }
    }
}
