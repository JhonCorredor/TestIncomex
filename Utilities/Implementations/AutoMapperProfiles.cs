using AutoMapper;
using Entity.Models;
using Entity.Dtos;
using Utilities.Interfaces;

namespace Utilities.Implementations
{
    /// <summary>
    /// Clase de perfiles para AutoMapper que define los mapeos entre entidades y DTOs.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;



        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AutoMapperProfiles"/> con el servicio de autenticación JWT.
        /// </summary>
        /// <param name="jwtAuthenticationService">Servicio de autenticación JWT.</param>
        public AutoMapperProfiles(IJwtAuthenticationService jwtAuthenticationService) : base()
        {
            _jwtAuthenticationService = jwtAuthenticationService;



            CreateMap<Product, ProductDto>().ReverseMap();


            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
           
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<Shipper, ShipperDto>().ReverseMap();
            CreateMap<Suppliers, SuppliersDto>().ReverseMap();



            // Configuración de perfiles base
            ConfigureBaseModelMappings();
            ConfigureBaseModelContactMappings();

            // Configuración específica para Category
            CreateMap<Category, CategoryDto>().ForMember(dest => dest.Picture, opt => opt.MapFrom(src =>
                    src.Picture != null ? ImageHelper.ConvertBytesToBase64(src.Picture) : string.Empty))
                .ReverseMap().ForMember(dest => dest.Picture, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.Picture) ? ImageHelper.ConvertBase64ToBytes(src.Picture) : null));


            // Configuración específica para Employee
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src =>
                    src.Photo != null ? ImageHelper.ConvertBytesToBase64(src.Photo) : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.Photo) ? ImageHelper.ConvertBase64ToBytes(src.Photo) : null));
                

            // Configuración específica para Suppliers
            CreateMap<Suppliers, SuppliersDto>()
                .IncludeBase<BaseModelContact, BaseModelContactDto>()
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage))
                .ReverseMap()
                .IncludeBase<BaseModelContactDto, BaseModelContact>()
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage));
        }

        /// <summary>
        /// Configuración de mapeos genéricos entre entidades que heredan de <see cref="BaseModel"/> y sus respectivos DTOs.
        /// </summary>
        private void ConfigureBaseModelMappings()
        {
            CreateMap<BaseModel, BaseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt));
            CreateMap<BaseDto, BaseModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt))
                .ForMember(dest => dest.UpdateAt, opt => opt.Ignore()) // El campo UpdateAt se ignora, ya que se manejará de otra manera.
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore()); // El campo DeletedAt se ignora.
        }
        /// <summary>
        /// Configuración de mapeos genéricos entre entidades que heredan de <see cref="BaseModelContact"/> y sus respectivos DTOs.
        /// </summary>
        private void ConfigureBaseModelContactMappings()
        {
            CreateMap<BaseModelContact, BaseModelContactDto>()
                .IncludeBase<BaseModel, BaseDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
                .ForMember(dest => dest.ContactTitle, opt => opt.MapFrom(src => src.ContactTitle))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax));

            CreateMap<BaseModelContactDto, BaseModelContact>()
                .IncludeBase<BaseDto, BaseModel>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
                .ForMember(dest => dest.ContactTitle, opt => opt.MapFrom(src => src.ContactTitle))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax));
        }
    }
}