using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity.Models;

namespace Entity.Contexts
{
    /// <summary>
    /// Clase que configura las entidades del modelo en la base de datos.
    /// Implementa <see cref="IEntityTypeConfiguration{TEntity}"/> para cada entidad,
    /// con el propósito de definir las reglas y restricciones a nivel de base de datos.
    /// </summary>
    public class ApplicationEntityConfigurations :
        IEntityTypeConfiguration<Suppliers>,
        IEntityTypeConfiguration<Customer>,
        IEntityTypeConfiguration<Product>,
        IEntityTypeConfiguration<Category>,
        IEntityTypeConfiguration<Employee>,
        IEntityTypeConfiguration<Order>,
        IEntityTypeConfiguration<OrderDetail>,
        IEntityTypeConfiguration<Shipper>
    {
        /// <summary>
        /// Configuración de la entidad Suppliers.
        /// Define la clave primaria y los índices de la entidad.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Suppliers> builder)
        {
            builder.HasKey(s => s.Id); // Clave primaria
            builder.HasIndex(s => s.CompanyName).HasDatabaseName("I1_CompanyName"); // Índice para CompanyName
            builder.HasIndex(s => s.Region).HasDatabaseName("I2_Region_Suppliers"); // Índice para Region
        }

        /// <summary>
        /// Configuración de la entidad Customer.
        /// Define la clave primaria y los índices de la entidad.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id); // Clave primaria
            builder.HasIndex(c => c.CompanyName).HasDatabaseName("I2_Customers_CompanyName"); // Índice para CompanyName
            builder.HasIndex(c => c.Region).HasDatabaseName("I1_Region"); // Índice para Region
        }

        /// <summary>
        /// Configuración de la entidad Product.
        /// Define la clave primaria, los índices y las relaciones con otras entidades.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id); // Clave primaria
            builder.HasIndex(p => p.ProductName).HasDatabaseName("I3_ProductName"); // Índice para ProductName
            builder.HasIndex(p => p.SupplierId).HasDatabaseName("FK1_SupplierId"); // Índice para SupplierID
            builder.HasIndex(p => p.CategoryId).HasDatabaseName("FK2_CategoryId"); // Índice para CategoryID

            // Relación con Category (cada Producto pertenece a una Categoría)
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            // Relación con Suppliers (cada Producto tiene un Proveedor)
            builder.HasOne<Suppliers>()
                .WithMany()
                .HasForeignKey(p => p.SupplierId);
        }

        /// <summary>
        /// Configuración de la entidad Category.
        /// Define la clave primaria y los índices de la entidad.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id); // Clave primaria
            builder.HasIndex(c => c.CategoryName).HasDatabaseName("U1_CategoryName"); // Índice único para CategoryName
        }

        /// <summary>
        /// Configuración de la entidad Employee.
        /// Define la clave primaria, los índices y las relaciones con otras entidades.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id); // Clave primaria
            builder.HasIndex(e => new { e.LastName, e.FirstName }).HasDatabaseName("I1_LastName_FirstName"); // Índice compuesto para LastName y FirstName
            builder.HasIndex(e => e.Region).HasDatabaseName("I2_Region_Employee"); // Índice para Region

            // Relación jerárquica de empleados (un empleado reporta a otro empleado)
            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(e => e.ReportsTo);
        }

        /// <summary>
        /// Configuración de la entidad Order.
        /// Define la clave primaria, los índices y las relaciones con otras entidades.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id); // Clave primaria para Orders
            builder.HasIndex(o => o.CustomerID).HasDatabaseName("FK1_CustomerID"); // Índice para CustomerID
            builder.HasIndex(o => o.EmployeeID).HasDatabaseName("FK2_EmployeeID"); // Índice para EmployeeID
            builder.HasIndex(o => o.ShipVia).HasDatabaseName("FK3_ShipVia"); // Índice para ShipVia (proveedor de envío)

            // Relación con Customer (cada Pedido tiene un Cliente)
            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerID);

            // Relación con Employee (cada Pedido tiene un Empleado que lo gestiona)
            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(o => o.EmployeeID);

            // Relación con Shipper (cada Pedido tiene un proveedor de envío)
            builder.HasOne<Shipper>()
                .WithMany()
                .HasForeignKey(o => o.ShipVia)
                .OnDelete(DeleteBehavior.Restrict); // Restricción en la eliminación de un Shipper
        }

        /// <summary>
        /// Configuración de la entidad OrderDetail.
        /// Define la clave primaria compuesta y las relaciones con otras entidades.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id); // Clave primaria
            builder.HasIndex(od => od.OrderID).HasDatabaseName("FK1_OrderID"); // Índice para OrderID
            builder.HasIndex(od => od.ProductID).HasDatabaseName("FK2_ProductID"); // Índice para ProductID

            // Relación con Order (cada detalle pertenece a un Pedido)
            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(od => od.OrderID);

            // Relación con Product (cada detalle tiene un Producto específico)
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(od => od.ProductID);
        }

        /// <summary>
        /// Configuración de la entidad Shipper.
        /// Define la clave primaria y los índices de la entidad.
        /// </summary>
        /// <param name="builder">Constructor utilizado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.HasKey(s => s.Id); // Clave primaria
            builder.HasIndex(s => s.CompanyName).HasDatabaseName("I1_Shippers_CompanyName"); // Índice para CompanyName
        }
    }
}
