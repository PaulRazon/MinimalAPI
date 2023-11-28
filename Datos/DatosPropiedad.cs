using PropiedadesMinimalApi.Modelos;

namespace PropiedadesMinimalApi.Datos
{
    public static class DatosPropiedad
    {
        public static List<Propiedad> listaPropiedades = new List<Propiedad> { 
            new Propiedad{IdPropiedad =1,Nombre="Casa Blanca",Descripcion="Es la de estados unidos",Ubicacion="Washintong D.C"
                ,Activa=true,FechaCreacion=DateTime.Now.AddDays(-10)},
            new Propiedad{IdPropiedad =2,Nombre="Casa Azul",Descripcion="Es la de estados Verdes",Ubicacion="Arizona D.C"
                ,Activa=false,FechaCreacion=DateTime.Now.AddDays(-10)},
            new Propiedad{IdPropiedad =3,Nombre="Casa Negra",Descripcion="Es la de estados Rojos",Ubicacion="California D.C"
                ,Activa=true,FechaCreacion=DateTime.Now.AddDays(-10)},
            new Propiedad{IdPropiedad =4,Nombre="Casa Rosa",Descripcion="Es la de estados Amarillos",Ubicacion="Washintong C.D"
                ,Activa=false,FechaCreacion=DateTime.Now.AddDays(-10)}

        };

    }
}
