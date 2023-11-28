using AutoMapper;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.DTOS;

namespace PropiedadesMinimalApi.Mapas
{
    public class ConfiguracionMapas:Profile
    {
        public ConfiguracionMapas()
        {
            CreateMap<Propiedad, CrearPropiedadDto>().ReverseMap();
            CreateMap<Propiedad, PropiedadDto>().ReverseMap();
        }
    }
}
