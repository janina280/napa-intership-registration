using AutoMapper;
using Services.Models;
namespace Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Port, DatabaseLayout.Models.Port>().ReverseMap();
        CreateMap<Ship, DatabaseLayout.Models.Ship>().ReverseMap();
        CreateMap<Voyage, DatabaseLayout.Models.Voyage>().ReverseMap();
    }
}