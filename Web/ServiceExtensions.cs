using Business.Implementations;
using Business.Interfaces;
using Data.Implementations;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            // Registrar servicios de negocios y de datos para Suppliers
            services.AddScoped<IBaseModelBusiness<Suppliers, SuppliersDto>, BaseModelBusiness<Suppliers, SuppliersDto>>();
            services.AddScoped<IBaseModelData<Suppliers, SuppliersDto>, BaseModelData<Suppliers, SuppliersDto>>();

            // Registrar servicios de negocios y de datos para Customer
            services.AddScoped<IBaseModelBusiness<Customer, CustomerDto>, BaseModelBusiness<Customer, CustomerDto>>();
            services.AddScoped<IBaseModelData<Customer, CustomerDto>, BaseModelData<Customer, CustomerDto>>();

            // Registrar servicios de negocios y de datos para Product
            services.AddScoped<IBaseModelBusiness<Product, ProductDto>, ProductBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IProductData, ProductData>();

            // Registrar servicios de negocios y de datos para Category
            services.AddScoped<IBaseModelBusiness<Category, CategoryDto>, BaseModelBusiness<Category, CategoryDto>>();
            services.AddScoped<IBaseModelData<Category, CategoryDto>, BaseModelData<Category, CategoryDto>>();

            // Registrar servicios de negocios y de datos para Employee
            services.AddScoped<IBaseModelBusiness<Employee, EmployeeDto>, BaseModelBusiness<Employee, EmployeeDto>>();
            services.AddScoped<IBaseModelData<Employee, EmployeeDto>, BaseModelData<Employee, EmployeeDto>>();

            // Registrar servicios de negocios y de datos para Order
            services.AddScoped<IBaseModelBusiness<Order, OrderDto>, BaseModelBusiness<Order, OrderDto>>();
            services.AddScoped<IBaseModelData<Order, OrderDto>, BaseModelData<Order, OrderDto>>();

            // Registrar servicios de negocios y de datos para OrderDetail
            services.AddScoped<IBaseModelBusiness<OrderDetail, OrderDetailDto>, BaseModelBusiness<OrderDetail, OrderDetailDto>>();
            services.AddScoped<IBaseModelData<OrderDetail, OrderDetailDto>, BaseModelData<OrderDetail, OrderDetailDto>>();

            // Registrar servicios de negocios y de datos para Shipper
            services.AddScoped<IBaseModelBusiness<Shipper, ShipperDto>, BaseModelBusiness<Shipper, ShipperDto>>();
            services.AddScoped<IBaseModelData<Shipper, ShipperDto>, BaseModelData<Shipper, ShipperDto>>();

            // Registrar servicios de negocios y de datos para OrderDetail
        //    services.AddScoped<IBaseModelBusiness<OrderDetail, OrderDetailDto>, BaseModelBusiness<OrderDetail, OrderDetailDto>>();
        //    services.AddScoped<IBaseModelData<OrderDetail, OrderDetailDto>, BaseModelData<OrderDetail, OrderDetailDto>>();
        }
    }
}
