using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Humanos.Queries.GetHumanoById
{
    public class GetHumanoByIdQuery : IRequest<HumanoDto>
    {
        public int Id { get; set; }
    }

    public class GetHumanoByIdQueryHandler : IRequestHandler<GetHumanoByIdQuery, HumanoDto>
    {
        private readonly IRepositoryAsync<Humano> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetHumanoByIdQueryHandler(IRepositoryAsync<Humano> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<HumanoDto> Handle(GetHumanoByIdQuery request, CancellationToken cancellationToken)
        {
            var record= await _repositoryAsync.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            var dto = _mapper.Map<HumanoDto>(record);

            return dto;
        }
    }
}
