using AutoMapper;
using UserManagementAPI.Infrastructure.Models;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserRequest, User>();
        }
    }
}
