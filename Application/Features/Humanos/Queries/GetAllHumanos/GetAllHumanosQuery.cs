using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Humanos.Queries.GetAllHumanos
{
    public class GetAllHumanosQuery  : IRequest<List<HumanoDto>>
    {

    }

    public class GetAllHumanosQueryHandler : IRequestHandler<GetAllHumanosQuery, List<HumanoDto>>
    {
        private readonly IRepositoryAsync<Humano> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllHumanosQueryHandler(IRepositoryAsync<Humano> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<List<HumanoDto>> Handle(GetAllHumanosQuery request, CancellationToken cancellationToken)
        {
            var lstHumanos = await _repositoryAsync.ListAsync(cancellationToken);
            if(!lstHumanos.Any())
                throw new KeyNotFoundException("No existen registros en la base de datos");

            var lstHumanosDto = _mapper.Map<List<HumanoDto>>(lstHumanos);

            return lstHumanosDto;
        }
    }
}
