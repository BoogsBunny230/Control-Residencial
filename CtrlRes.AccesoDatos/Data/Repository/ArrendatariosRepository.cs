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
    public class ArrendatariosRepository : Repository<Arrendatarios>, IArrendatariosRepository
    {
        private readonly ApplicationDbContext _db;

        public ArrendatariosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaArrendatarios()
        {
            return _db.Arrendatarios.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }
            );
        }

        public void Update(Arrendatarios arrendatarios)
        {
            var ObjDesdeDb = _db.Arrendatarios.FirstOrDefault(s => s.Id == arrendatarios.Id);
            ObjDesdeDb.Nombre = arrendatarios.Nombre;
            ObjDesdeDb.Celular = arrendatarios.Celular;
            ObjDesdeDb.Vehiculo = arrendatarios.Vehiculo;
            ObjDesdeDb.Vivienda_Id = arrendatarios.Vivienda_Id;

            _db.SaveChanges();
        }
    }
}
