using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Microsoft.Extensions.Configuration;

namespace Data.Implementations
{
    public class ProductData : BaseModelData<Product, ProductDto>, IProductData
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public ProductData(ApplicationDbContext applicationDbContext, IConfiguration configuration, IMapper mapper) : base(applicationDbContext, configuration, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una entidad por su identificador único.
        /// </summary>
        public override async Task<ProductDto> GetById(int Id)
        {
            var sql = @"SELECT 
                    pro.""Id"",                                   
                    pro.""ProductName"",
                    pro.""SupplierId"",
                    pro.""CategoryId"",
                    pro.""QuantityPerUnit"",
                    pro.""UnitPrice"",
                    pro.""UnitsInStock"",
                    pro.""UnitsOnOrder"",
                    pro.""ReorderLevel"",
                    pro.""Discontinued"",
                    cat.""Picture"" AS ""PictureCategory""
                FROM public.""Product"" AS pro
                INNER JOIN public.""Category"" AS cat ON pro.""CategoryId"" = cat.""Id""
                WHERE pro.""Id"" = @Id AND pro.""DeletedAt"" IS NULL";

            return await _applicationDbContext.QueryFirstOrDefaultAsync<ProductDto>(sql, new { Id = Id });
        }
    }

}

