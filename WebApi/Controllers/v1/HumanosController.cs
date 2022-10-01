using Application.DTOs;
using Application.Features.Humanos.Commands.CreateHumanoCommand;
using Application.Features.Humanos.Commands.UpdateHumanoCommand;
using Application.Features.Humanos.Queries.GetAllHumanos;
using Application.Features.Humanos.Queries.GetHumanoById;
using Application.Wrappers;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class HumanosController : BaseApiController
    {
        /// <summary>
        /// GET Array Humanos Mock
        /// </summary>
        /// <returns>Retorna un arreglo de objeto de tipo humanos</returns>
        [HttpGet("HumanosMock")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HumanoDto[]))]
        public async Task<IActionResult> GetHumanosMock()
        {
            HumanoDto[] humanosMock = new HumanoDto[10];
            var faker = new Faker<HumanoDto>("es_MX")
                .RuleFor(x => x.Id, f => f.Random.Number(1, 100))
                .RuleFor(x => x.Nombre, f => f.Name.FirstName())
                .RuleFor(x => x.Sexo, f => f.Random.ListItem(new List<char> { 'F', 'M' }))
                .RuleFor(x => x.Edad, f => f.Random.Number(1, 99))
                .RuleFor(x => x.Peso, f => f.Random.Decimal(3.5M, 150M))
                .RuleFor(x => x.Altura, f => f.Random.Decimal(0.5M, 2.5M));
            humanosMock = faker.Generate(10).ToArray();
            return Ok(humanosMock);
        }

        /// <summary>
        /// GET obtener toda la tabla humanos
        /// </summary>
        /// <returns>Retorna todos los registros de la tabla</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<HumanoDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        public async Task<IActionResult> GetAllHumanos()
        {
            return Ok(await Mediator.Send(new GetAllHumanosQuery()));
        }

        /// <summary>
        /// GET obtener un registro de la tabal humano por id
        /// </summary>
        /// <param name="id">Id del registro buscado en la base de datos</param>
        /// <returns>Retorna el registro que coincide con el id solicitado</returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HumanoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        public async Task<IActionResult> GetHumanoById(int id)
        {
            return Ok(await Mediator.Send(new GetHumanoByIdQuery() { Id=id}));
        }

        /// <summary>
        /// POST crear un registro en la tabla humano
        /// </summary>
        /// <param name="command">Objeto de tipo CreateHumanoCommnad</param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        public async Task<IActionResult> Post([FromBody] CreateHumanoCommand command)
        {
            
            return Created(Url.RouteUrl(0)!, await Mediator.Send(command));
        }

        /// <summary>
        /// PUT actualizar un registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        public async Task<IActionResult> Put(int id, UpdateHumanoCommand command)
        {
            if (id != command.Id)
                return BadRequest("El id no coincide con el cuerpo de la petición");

            return Ok(await Mediator.Send(command));
        }
    }
}
