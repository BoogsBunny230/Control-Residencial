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
    public class UsuariosRepository : Repository<Usuarios>, IUsuariosRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuariosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaUsuarios()
        {
            return _db.Usuarios.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.IdUsu.ToString()
            }
            );
        }

        public void Update(Usuarios usuarios)
        {
            var ObjDesdeDb = _db.Usuarios.FirstOrDefault(s => s.IdUsu == usuarios.IdUsu);
            ObjDesdeDb.Nombre = usuarios.Nombre;
            ObjDesdeDb.Contrasena = usuarios.Contrasena;
            ObjDesdeDb.UserName = usuarios.UserName;

            _db.SaveChanges();
        }
    }
}
