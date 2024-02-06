using AutoMapper;
using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;

namespace Dev_Dashboard.MapperProfiler
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UserDetail, UserDetailDTO>();
            CreateMap<UserDetailDTO, UserDetail>();
        }
    }
}
