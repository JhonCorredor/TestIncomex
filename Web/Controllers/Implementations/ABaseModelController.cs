using Entity.Dtos;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implementations
{
    public abstract class ABaseModelController<T, D> : ControllerBase, IBaseModelController<T, D> where T : BaseModel where D : BaseDto
    {
        public abstract Task<ActionResult> Delete(int id);

        public abstract Task<ActionResult<IEnumerable<D>>> GetAllSelect();

        public abstract Task<ActionResult<D>> GetByCode(string code);

        public abstract Task<ActionResult<D>> GetById(int id);

        public abstract Task<ActionResult<IEnumerable<D>>> GetDataTable([FromQuery] QueryFilterDto filters);

        public abstract Task<ActionResult<D>> Save(D dto);

        public abstract Task<ActionResult> Update(D dto);
    }
}
