using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Utilities.Implementations;
using Utilities.Interfaces;
using Web;

var builder = WebApplication.CreateBuilder(args);


// Definir la pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:44355/") // Dominios permitidos
              .AllowAnyHeader() // Permitir cualquier encabezado
              .AllowAnyMethod() // Permitir cualquier m�todo (GET, POST, etc.)
              .AllowCredentials(); // Permitir credenciales
    });
});

// Configuraci�n de URL espec�fica
builder.WebHost.UseUrls("http://*:5000");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configuraci�n del contexto de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IncomexDB")));

// A�adir servicios al contenedor.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configuraci�n para evitar referencias c�clicas en la serializaci�n JSON.
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Configurar Swagger/OpenAPI para la documentaci�n de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar servicios personalizados desde un archivo de extensi�n
ServiceExtensions.AddCustomServices(builder.Services);

// Configuraci�n del servicio de autenticaci�n JWT como Singleton
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

// Configuraci�n del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Usar Swagger para documentaci�n de la API solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuraci�n de CORS para permitir cualquier origen, m�todo y encabezado
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Configuraci�n de autenticaci�n
app.UseAuthentication();

// Configuraci�n de Swagger en la aplicaci�n
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Incomex V1");
});

// Redirecci�n de HTTPS
app.UseHttpsRedirection();

// Configuraci�n del enrutamiento
app.UseRouting();

// Configuraci�n de autorizaci�n
app.UseAuthorization();

// Mapeo de controladores a rutas
app.MapControllers();

// Configuraci�n espec�fica de CORS 
app.UseCors("_myAllowSpecificOrigins");

// Ejecutar la aplicaci�n
app.Run();
