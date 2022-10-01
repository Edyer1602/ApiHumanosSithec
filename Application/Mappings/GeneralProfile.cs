using Application.DTOs;
using Application.Features.Humanos.Commands.CreateHumanoCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    internal class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateHumanoCommand, Humano>();
            #endregion

            #region DTOs
            CreateMap<Humano, HumanoDto>();
            #endregion
        }
    }
}
