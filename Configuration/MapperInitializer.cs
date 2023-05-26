using AutoMapper;
using shop.Models;
namespace shop.Configuration;

public class MapperInitializer : Profile
{
    public MapperInitializer()
    {
        CreateMap<ApiUser, UserDTO>().ReverseMap();
        CreateMap<Roles, RoleDTO>().ReverseMap();
    }
}