using Data.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Data.Implementations
{
    public abstract class ABaseModelData<T, D> : IBaseModelData<T, D> where T : BaseModel where D: BaseDto
    {
        public abstract Task<int> Delete(int id);

        public abstract Task<IEnumerable<D>> GetAllSelect();


        public abstract Task<T> GetById(int id);

        public abstract Task<IEnumerable<D>> GetDataTable(QueryFilterDto filters);

        public abstract Task<T> Save(T entity);

        public abstract Task Update(T entity);

        
    }
}
