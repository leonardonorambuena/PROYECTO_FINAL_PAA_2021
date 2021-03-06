using System;
using System.Data.Entity;
using System.Linq;

namespace PAA_MVC_2021.Models.Entities
{
    public class ApplicationDbContext : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'ApplicationDbContext' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'PAA_MVC_2021.Models.Entities.ApplicationDbContext' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'ApplicationDbContext'  en el archivo de configuración de la aplicación.
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPlatform> ProductPlatforms { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SaleLine> SaleLines { get; set; }
    }


}