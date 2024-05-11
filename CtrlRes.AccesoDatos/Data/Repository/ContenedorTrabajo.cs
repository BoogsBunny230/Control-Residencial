using CtrlRes.AccesoDatos.Data.Repository;
using CtrlRes.AccesoDatos.Data.Repository.IRepository;
using CtrlRes.Data;
using CtrlRes.Data.Migrations;
using CtrlRes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlRes.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db) 
        {
            _db = db;

            //Catálogos

            Privadas = new PrivadasRepository(_db);
            Propietarios = new PropietariosRepository(_db);
            Arrendatarios = new ArrendatariosRepository(_db);
            TAGs = new TAGsRepository(_db);
            Proveedores = new ProveedoresRepository(_db);

            //Herramientas

            Herramientas = new HerramientasRepository(_db);
            Usuarios = new UsuariosRepository(_db);
			ArchivosPDF = new ArchivosPDFRepository(_db);
			//CodigoUrbano = new CodigoUrbanoRepository(_db);

			//Módulos

			//Notificaciones = new NotificacionesRepository(_db);
			//Pagos = new PagosRepository(_db);

        }

        //Catálogos

        public IPrivadasRepository Privadas { get; private set; }
        public IPropietariosRepository Propietarios { get; private set; }
        public IArrendatariosRepository Arrendatarios { get; private set; }
        public ITAGsRepository TAGs { get; private set; }
        public IProveedoresRepository Proveedores { get; private set; }

        //Herramientas

        public IHerramientasRepository Herramientas { get; private set; }
        public IUsuariosRepository Usuarios { get; private set; }
		public IArchivosPDFRepository ArchivosPDF { get; private set; }
		//public ICodigoUrbanoRepository CodigoUrbano { get; private set; }

		//Módulos

		//public INotificacionesRepository Notificaciones { get; private set; }
		//public IPagosRepository Pago { get; private set; }

        public void Dispose()
        {
            _db.Dispose(); 
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
