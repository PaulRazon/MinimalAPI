using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Modelos;

namespace PropiedadesMinimalApi.Datos
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Propiedad> Propiedad { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Propiedad>().HasData(
                    new Propiedad
                    {
                        IdPropiedad = 1,
                        Nombre = "Casa Blanca",
                        Descripcion = "Es la de estados unidos",
                        Ubicacion = "Washintong D.C",
                        Activa = true,
                        FechaCreacion = DateTime.Now
                    },
                    new Propiedad
                    {
                        IdPropiedad = 2,
                        Nombre = "Casa Azul",
                        Descripcion = "Es la de estados Verdes",
                        Ubicacion = "Arizona D.C"
                        ,
                        Activa = false,
                        FechaCreacion = DateTime.Now
                    },
                    new Propiedad
                    {
                        IdPropiedad = 3,
                        Nombre = "Casa Negra",
                        Descripcion = "Es la de estados Rojos",
                        Ubicacion = "California D.C"
                        ,
                        Activa = true,
                        FechaCreacion = DateTime.Now
                    },
                    new Propiedad
                    {
                        IdPropiedad = 4,
                        Nombre = "Casa Rosa",
                        Descripcion = "Es la de estados Amarillos",
                        Ubicacion = "Washintong C.D"
                        ,
                        Activa = false,
                        FechaCreacion = DateTime.Now
                    }
                );
        }
    }
}
