﻿using Business.Interfaces;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Implementations
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseModelController<T, D> : ABaseModelController<T, D> where T : BaseModel where D : BaseDto
    {
        private readonly IBaseModelBusiness<T, D> _business;

        public BaseModelController(IBaseModelBusiness<T, D> business)
        {
            _business = business;
        }

        /// <summary>
        /// Obtiene una lista de todos los registros disponibles en forma de DTOs.
        /// </summary>
        /// <returns>Una lista de DTOs que representan todos los registros almacenados.</returns>
        [HttpGet()]
        public override async Task<ActionResult<IEnumerable<D>>> GetAll([FromQuery] PaginationDto pagination)
        {
            try
            {
                var data = await _business.GetAll(pagination);

                if (data == null)
                {
                    var responseNull = new ApiResponse<IEnumerable<D>>(null!, false, "Registro no encontrado");
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<IEnumerable<D>>(data, true, "Ok");

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<D>>(null, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtiene un registro por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del registro a obtener.</param>
        /// <returns>El DTO correspondiente al registro encontrado.</returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<D>> GetById(int id)
        {
            try
            {
                var data = await _business.GetById(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<D>(null!, false, "Registro no encontrado");
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<D>(data, true, "Ok");

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<D>>(null, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Guarda un nuevo registro en la base de datos.
        /// </summary>
        /// <param name="dto">El DTO con la información del registro a guardar.</param>
        /// <returns>El DTO correspondiente al registro guardado, junto con un estado 201 Created.</returns>
        [HttpPost]
        public override async Task<ActionResult<D>> Save(D dto)
        {
            try
            {
                D dtoSaved = await _business.Save(dto);
                var response = new ApiResponse<D>(dtoSaved, true, "Registro almacenado exitosamente");

                return new CreatedAtRouteResult(new { id = dtoSaved.Id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<D>>(null!, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="dto">El DTO con la información actualizada del registro.</param>
        /// <returns>Un estado 200 OK si la actualización fue exitosa.</returns>
        [HttpPut]
        public override async Task<ActionResult> Update(D dto)
        {
            try
            {
                await _business.Update(dto);

                var response = new ApiResponse<D>(null!, true, "Registro actualizado exitosamente");

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<D>>(null!, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Realiza un eliminado lógico del registro especificado por su identificador.
        /// </summary>
        /// <param name="id">Identificador único del registro a eliminar.</param>
        /// <returns>Un estado 200 OK si el registro fue eliminado exitosamente, de lo contrario un error.</returns>
        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id)
        {
            try
            {
                int registrosAfectados = await _business.Delete(id);
                if (registrosAfectados == 0)
                {
                    var errorResponse = new ApiResponse<IEnumerable<D>>(null, false, "Registro no eliminado!");
                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }
                var successResponse = new ApiResponse<D>(null!, true, "Registro eliminado exitosamente");
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<IEnumerable<D>>(null!, false, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
