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
    public class ArchivosPDFRepository : Repository<ArchivosPDF>, IArchivosPDFRepository
    {
        private readonly ApplicationDbContext _db;

        public ArchivosPDFRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaArchivosPDF()
        {
            return _db.ArchivosPDF.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }
            );
        }

        public void Update(ArchivosPDF archivosPDF)
        {
            var ObjDesdeDb = _db.ArchivosPDF.FirstOrDefault(s => s.Id == archivosPDF.Id);
            ObjDesdeDb.Nombre = archivosPDF.Nombre;
            ObjDesdeDb.Archivo = archivosPDF.Archivo;

            _db.SaveChanges();
        }
    }
}
