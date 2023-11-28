using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropiedadesMinimalApi.Datos;
using PropiedadesMinimalApi.Mapas;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.DTOS;
using FluentValidation;
using System.Net;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//configurar conexion a BD
builder.Services.AddDbContext<AplicationDbContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Añadir AutoMapper
builder.Services.AddAutoMapper(typeof(ConfiguracionMapas));

//añadir validaciones
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

////´Puntos de entrada primero endpoint
//app.MapGet("/saludo/{id:int}", (int id) => {
//    return Results.Ok("id:" + id);
//    //return Results.BadRequest("Error al solictar");
//});

//app.MapPost("/saludo2", () => {
//   return "";
//});

//obtener todas las propiedades
app.MapGet("/api/propiedades", async (AplicationDbContext _bd, ILogger<Program> logger) =>
{
    RespuestasApi respuesta = new();
    //Usar logger que ya esta como inyeccion de dependencias
    logger.Log(LogLevel.Information, "Carga todas las propiedades");
    //respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Resultado = _bd.Propiedad;
    respuesta.Success = true;
    respuesta.statusCode = HttpStatusCode.OK;
    return Results.Ok(respuesta);
}).WithName("ObtenerPropiedades").Produces< RespuestasApi> (200);

//obtener un registro individual
app.MapGet("/api/propiedades/{id:int}", async (AplicationDbContext _bd,int id) =>
{
    RespuestasApi respuesta = new();
    //respuesta.Resultado = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id);
    respuesta.Resultado = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == id);
    respuesta.Success = true;
    respuesta.statusCode = HttpStatusCode.OK;
    return Results.Ok(respuesta);
    //return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad").Produces<RespuestasApi>(200);

//POST
//Crear propiedad
app.MapPost("/api/propiedades/", async (AplicationDbContext _bd, IMapper _mapper,
    IValidator<CrearPropiedadDto>_validacion, [FromBody] CrearPropiedadDto crearPropiedadDto) => {
    RespuestasApi respuesta = new() { 
        Success=false,statusCode=HttpStatusCode.BadRequest
    };
    var resultadoValidaciones = await _validacion.ValidateAsync(crearPropiedadDto);
    //validar id de propiedad y que el nombre no este vacio
    if (!resultadoValidaciones.IsValid) {
        
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }
    //validar si el nombre de la propiedad ya existe
    if (await _bd.Propiedad.FirstOrDefaultAsync(p => p.Nombre.ToLower() == crearPropiedadDto.Nombre.ToLower()) != null) {
            respuesta.Errores.Add("El nombre de la propiedad ya existe");
            return Results.BadRequest(respuesta);
    }
    //Propiedad propiedad = new Propiedad() {
    //    Nombre = crearPropiedadDto.Nombre,
    //    Descripcion = crearPropiedadDto.Descripcion,
    //    Ubicacion = crearPropiedadDto.Ubicacion,
    //    Activa = crearPropiedadDto.Activa

    //};
    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDto);
    await _bd.Propiedad.AddAsync(propiedad);
    await _bd.SaveChangesAsync();
    //propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;
    //DatosPropiedad.listaPropiedades.Add(propiedad);
    //return Results.Ok(DatosPropiedad.listaPropiedades);

    //PropiedadDto propiedadDto = new PropiedadDto() {
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa
    //};
    PropiedadDto propiedadDto  = _mapper.Map<PropiedadDto>(propiedad);
    //return Results.CreatedAtRoute("ObtenerPropiedad",new {id= propiedad.IdPropiedad}, propiedadDto);
    respuesta.Resultado = propiedadDto;
    respuesta.Success = true;
    respuesta.statusCode = HttpStatusCode.Created;
    return Results.Ok(respuesta);
}).WithName("CrearPropiedad").Accepts<CrearPropiedadDto>("application/json").Produces<RespuestasApi>(201).Produces(400);




//PUT
//Crear propiedad
app.MapPut("/api/propiedades/", async (AplicationDbContext _bd,IMapper _mapper,
    IValidator<ActualizarPropiedadDto> _validacion, [FromBody] ActualizarPropiedadDto actualizarPropiedadDto) => {
        RespuestasApi respuesta = new()
        {
            Success = false,
            statusCode = HttpStatusCode.BadRequest
        };
        var resultadoValidaciones = await _validacion.ValidateAsync(actualizarPropiedadDto);
        //validar id de propiedad y que el nombre no este vacio
        if (!resultadoValidaciones.IsValid)
        {

            respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
            return Results.BadRequest(respuesta);
        }

        Propiedad propiedadDesdeBD = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == actualizarPropiedadDto.IdPropiedad);
        
        propiedadDesdeBD.Nombre = actualizarPropiedadDto.Nombre;
        propiedadDesdeBD.Descripcion = actualizarPropiedadDto.Descripcion;
        propiedadDesdeBD.Ubicacion = actualizarPropiedadDto.Ubicacion;
        propiedadDesdeBD.Activa = actualizarPropiedadDto.Activa;
        await _bd.SaveChangesAsync();
        respuesta.Resultado = _mapper.Map<Propiedad>(propiedadDesdeBD);
        respuesta.Success = true;
        respuesta.statusCode = HttpStatusCode.Created;
        return Results.Ok(respuesta);
    }).WithName("ActualizarPropiedad").Accepts<ActualizarPropiedadDto>("application/json").Produces<RespuestasApi>(200).Produces(400);

//DELETE
//BORRAR PROPIEDAD
app.MapDelete("/api/propiedades/{id:int}",async (AplicationDbContext _bd,int id) =>
{
    RespuestasApi respuesta = new()
    {
        Success = false,
        statusCode = HttpStatusCode.BadRequest
    };
    //obetener el id de la propiedad a eliminar
    Propiedad propiedadDesdeBD = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == id);

    if (propiedadDesdeBD != null)
    {
        _bd.Propiedad.Remove(propiedadDesdeBD);
        await _bd.SaveChangesAsync();
        respuesta.Success = true;
        respuesta.statusCode = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }
    else {
        respuesta.Errores.Add("El ID de la propiedad es invalido");
        return Results.BadRequest(respuesta);
    }


    //return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
});

app.UseHttpsRedirection();

app.Run();
