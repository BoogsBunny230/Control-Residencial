using CtrlRes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CtrlRes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

		public string Conexion { get; }
		public ApplicationDbContext(string valor)
		{
			Conexion = valor;
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		public DbSet<ApplicationUser> ApplicationUser { get; set; }

		//Catálogos

		public DbSet<Privadas> Privadas { get; set; }
        public DbSet<Viviendas> Viviendas { get; set; }
        public DbSet<Propietarios> Propietarios { get; set; }
        public DbSet<Arrendatarios> Arrendatarios { get; set; }
        public DbSet<TAGs> TAGs { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }

        //Herramientas

        public DbSet<Herramientas> Herramientas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<RegistroLecturas> RegistroLecturas { get; set; }
        public DbSet<Lecturas> Lecturas { get; set; }
        public DbSet<ArchivosLecturas> ArchivoLecturas { get; set; }
        public DbSet<Mantenimientos> Mantenimientos { get; set; }
        public DbSet<MantenimientosConceptos> MantenimientosConceptos { get; set; }
        public DbSet<Sanciones> Sanciones { get; set; }
        public DbSet<SancionesConceptos> SancionesConceptos { get; set; }
		public DbSet<ArchivosPDF> ArchivosPDF { get; set; }
        //public DbSet<CodigoUrbano> CodigoUrbano { get; set; }

        //Módulos

        //public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<ArchivosPagos> ArchivosPagos { get; set; }
        public DbSet<Asambleas> Asambleas { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<Visitas> Visitas { get; set; }
        public DbSet<Comunidad> Comunidad { get; set; }
        public DbSet<Egresos> Egresos { get; set; }


    }
}