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
    public class PropietariosRepository : Repository<Propietarios>, IPropietariosRepository
    {
        private readonly ApplicationDbContext _db;

        public PropietariosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaPropietarios()
        {
            return _db.Propietarios.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }
            );
        }

        public void Update(Propietarios propietarios)
        {
            var ObjDesdeDb = _db.Propietarios.FirstOrDefault(s => s.Id == propietarios.Id);
            ObjDesdeDb.Nombre = propietarios.Nombre;
            ObjDesdeDb.Celular = propietarios.Celular;
            ObjDesdeDb.Vehiculo = propietarios.Vehiculo;
            ObjDesdeDb.Vivienda_Id = propietarios.Vivienda_Id;

            _db.SaveChanges();
        }
    }
}
