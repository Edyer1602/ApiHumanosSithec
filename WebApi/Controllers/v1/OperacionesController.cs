using Application.Features.Operaciones.GetResultOperacion;
using Application.Parameters;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OperacionesController : BaseApiController
    {
        /// <summary>
        /// GET operacion matemática con argumentos por header
        /// </summary>
        /// <param name="argumento1">Primer argumento de la operación</param>
        /// <param name="argumento2">Segundo argumento de la operación</param>
        /// <param name="operacion">Operador</param>
        /// <returns>Retorna el resultado de la operación indicada</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        public async Task<IActionResult> Get([FromHeader(Name = "argumento1")] decimal argumento1, [FromHeader(Name = "argumento2")] decimal argumento2, [FromHeader(Name = "operacion")] string operacion)
        {
            return Ok(await Mediator.Send(new GetResultOperacionQuery()
            {
                argumento1 = argumento1,
                argumento2 = argumento2,
                operacion = operacion
            }));
        }

        /// <summary>
        /// POST operacion matemática
        /// </summary>
        /// <param name="parameters">Objeto de tipo OperacionParameters</param>
        /// <returns>Retorna el resultado de la operación indicada</returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        public async Task<IActionResult> Post([FromBody] OperacionParameters parameters)
        {
            return Ok(await Mediator.Send(new GetResultOperacionQuery()
            {
                argumento1 = parameters.argumento1,
                argumento2 = parameters.argumento2,
                operacion = parameters.operacion
            }));
        }
    }
}
