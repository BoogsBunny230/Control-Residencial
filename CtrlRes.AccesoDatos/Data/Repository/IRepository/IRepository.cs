using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CtrlRes.AccesoDatos.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        //Métodos comunes o genericos a cada (cualquier) entidad (Categoria, articulo, sliders, etc)
        //Para obtener un registro
        T Get(string id); //cambie Int por string
                          //Obtener todos los registros (completos o filtrados)
        T GetFolio(int id); //cambie Int por string

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        //Obtener el primero o por defecto
        T GetFirstOrDefault(
             Expression<Func<T, bool>> filter = null,
             string includeProperties = null
            );
        //Agregar entidad a base de datos
        void add(T entity);

        //Remover entidad por Id
        void Remove(string id);

        //Remover pero con parámetros diferentes que recibe toda la clase
        //Remover pasando por toda la entidad
        void Remove(T entity);

    }
}
