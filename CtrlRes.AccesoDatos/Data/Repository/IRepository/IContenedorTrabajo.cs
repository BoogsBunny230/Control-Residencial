using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtrlRes.AccesoDatos.Data;

namespace CtrlRes.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        //Catálogos

        IPrivadasRepository Privadas { get; }
        IPropietariosRepository Propietarios { get; }
        IArrendatariosRepository Arrendatarios { get; }
        ITAGsRepository TAGs { get; }
        IProveedoresRepository Proveedores { get; }

         
        //Herramientas

        IHerramientasRepository Herramientas { get; }
        IUsuariosRepository Usuarios { get; }
        ////ICodigoUrbanoRepository CodigoUrbano { get; }

        //Módulos
        ////INotificacionesRepository Notificaciones { get; }
        //IPagosRepository Pagos { get; }

        void Save();
    }
}
