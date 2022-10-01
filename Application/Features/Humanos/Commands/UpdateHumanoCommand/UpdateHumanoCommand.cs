using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Features.Humanos.Commands.UpdateHumanoCommand
{
    public class UpdateHumanoCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public char Sexo { get; set; }
        public int Edad { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
    }

    public class UpdateHumanoCommandHandler : IRequestHandler<UpdateHumanoCommand, int>
    {
        private readonly IRepositoryAsync<Humano> _repositoryAsync;

        public UpdateHumanoCommandHandler(IRepositoryAsync<Humano> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<int> Handle(UpdateHumanoCommand request, CancellationToken cancellationToken)
        {
            var existeRegistro = await _repositoryAsync.ListAsync(new HumanoSpecification(request.Nombre,
                                                                                           request.Sexo,
                                                                                           request.Edad,
                                                                                           request.Altura,
                                                                                           request.Peso), cancellationToken);
            if (existeRegistro.Any() && existeRegistro.First().Id != request.Id)
            {
                throw new ApiException($"Existe un registro con las mismas características en la base de datos con el id {existeRegistro.First().Id}");
            }

            var record = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (record == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            record.Nombre = request.Nombre;
            record.Sexo = record.Sexo;
            record.Edad = request.Edad;
            record.Altura = request.Altura;
            record.Peso = request.Peso;

            await _repositoryAsync.UpdateAsync(record, cancellationToken);

            return record.Id;
        }
    }
}
