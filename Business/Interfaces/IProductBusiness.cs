using Entity.Dtos;
using Entity.Models;

namespace Business.Interfaces
{
    public interface IProductBusiness : IBaseModelBusiness<Product, ProductDto>
    {
        public Task GenerateProductsAsync(int quantity, int categoryId1, int categoryId2);

    }
}
