using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace Entity.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor de la clase `ApplicationDbContext`. Configura el contexto con las opciones proporcionadas y la configuración de la aplicación.
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configura el modelo de la base de datos usando convenciones, relaciones y configuraciones personalizadas.
        /// Se aplica la configuración desde el ensamblado actual y se inicializan datos de ejemplo.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DataInicial.Data(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Configura el contexto para permitir el registro de datos sensibles, útil durante la depuración para ver valores de parámetros en los logs.
        /// Esto puede ser útil para el desarrollo, pero debe deshabilitarse en producción para evitar problemas de seguridad.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // otras configuraciones...
        }

        /// <summary>
        /// Define convenciones personalizadas para el modelo. Establece que todas las propiedades de tipo `decimal` tendrán una precisión de (18, 2).
        /// </summary>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        /// <summary>
        /// Sobrescribe el método `SaveChanges` para incluir lógica de auditoría antes de guardar los cambios en la base de datos.
        /// </summary>
        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        /// <summary>
        /// Sobrescribe el método `SaveChangesAsync` para incluir lógica de auditoría antes de guardar los cambios de manera asíncrona.
        /// </summary>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Realiza una consulta SQL utilizando Dapper y devuelve una colección de objetos del tipo especificado.
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }

        /// <summary>
        /// Realiza una consulta SQL utilizando Dapper y devuelve el primer objeto del tipo especificado o un valor predeterminado si no se encuentra.
        /// </summary>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }

        /// <summary>
        /// Método que garantiza la auditoría de los cambios, detecta y rastrea cambios en las entidades antes de realizar operaciones en la base de datos.
        /// </summary>
        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
        }

        //Operational
        //public DbSet<Alert> Alerts => Set<Alert>();


        /// <summary>
        /// Comando personalizado de Dapper para ser utilizado con Entity Framework Core.
        /// </summary>
        public readonly struct DapperEFCoreCommand : IDisposable
        {
            /// <summary>
            /// Constructor que inicializa el comando con los detalles proporcionados.
            /// </summary>
            /// <param name="context">El contexto de la base de datos.</param>
            /// <param name="text">Texto de la consulta SQL.</param>
            /// <param name="parameters">Parámetros para la consulta.</param>
            /// <param name="timeout">Tiempo de espera del comando.</param>
            /// <param name="type">Tipo de comando SQL.</param>
            /// <param name="ct">Token de cancelación.</param>
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct
                );
            }

            public CommandDefinition Definition { get; }

            /// <summary>
            /// Implementación vacía de Dispose, ya que no se utiliza ningún recurso no administrado en este comando.
            /// </summary>
            public void Dispose()
            {
                // No se necesitan acciones adicionales para liberar recursos en este caso
            }
        }
    }
}
