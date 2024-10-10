using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Utilities.Implementations;
using Utilities.Interfaces;
using Web;

var builder = WebApplication.CreateBuilder(args);


// Definir la política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:44355/") // Dominios permitidos
              .AllowAnyHeader() // Permitir cualquier encabezado
              .AllowAnyMethod() // Permitir cualquier método (GET, POST, etc.)
              .AllowCredentials(); // Permitir credenciales
    });
});

// Configuración de URL específica
builder.WebHost.UseUrls("http://*:5000");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configuración del contexto de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IncomexDB")));

// Añadir servicios al contenedor.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configuración para evitar referencias cíclicas en la serialización JSON.
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Configurar Swagger/OpenAPI para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar servicios personalizados desde un archivo de extensión
ServiceExtensions.AddCustomServices(builder.Services);

// Configuración del servicio de autenticación JWT como Singleton
builder.Services.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>(provider =>
{
    // Obtener la clave directamente del appsettings.json para el JWT
    var key = builder.Configuration["JwtConfig:Key"];
    return new JwtAuthenticationService(key);
});

// Crear una instancia de AutoMapperProfiles con la instancia de IJwtAuthenticationService
var jwtAuthenticationService = builder.Services.BuildServiceProvider().GetService<IJwtAuthenticationService>();
var autoMapperProfiles = new AutoMapperProfiles(jwtAuthenticationService);

// Registrar AutoMapper con la instancia de AutoMapperProfiles
builder.Services.AddAutoMapper(_ => _.AddProfile(autoMapperProfiles));

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Usar Swagger para documentación de la API solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de CORS para permitir cualquier origen, método y encabezado
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Configuración de autenticación
app.UseAuthentication();

// Configuración de Swagger en la aplicación
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Incomex V1");
});

// Redirección de HTTPS
app.UseHttpsRedirection();

// Configuración del enrutamiento
app.UseRouting();

// Configuración de autorización
app.UseAuthorization();

// Mapeo de controladores a rutas
app.MapControllers();

// Configuración específica de CORS 
app.UseCors("_myAllowSpecificOrigins");

// Ejecutar la aplicación
app.Run();
