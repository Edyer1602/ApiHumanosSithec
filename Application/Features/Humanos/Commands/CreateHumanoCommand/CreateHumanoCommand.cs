using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Humanos.Commands.CreateHumanoCommand
{
    public class CreateHumanoCommand : IRequest<int>
    {
        public string Nombre { get; set; }
        public char Sexo { get; set; }
        public int Edad { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
    }

    public class CreateHumanoCommandHandler : IRequestHandler<CreateHumanoCommand, int>
    {
        private readonly IRepositoryAsync<Humano> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateHumanoCommandHandler(IRepositoryAsync<Humano> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateHumanoCommand request, CancellationToken cancellationToken)
        {
            var existeHumano = await _repositoryAsync.ListAsync(new HumanoSpecification(request.Nombre,
                                                                                        char.ToUpper(request.Sexo),
                                                                                        request.Edad,
                                                                                        request.Altura,
                                                                                        request.Peso), cancellationToken);
            if (existeHumano.Any())
            {
                throw new ApiException("Ya existe un registro con los mismo datos en la base de datos");
            }
            request.Sexo = char.ToUpper(request.Sexo);
            var newRecord = _mapper.Map<Humano>(request);
            var data = await _repositoryAsync.AddAsync(newRecord,cancellationToken);
            return data.Id;
        }
    }
}
