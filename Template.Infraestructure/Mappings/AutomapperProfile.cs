using AutoMapper;
using Template.Core.DTOs;
using Template.Core.Entities;

namespace Template.Infraestructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
