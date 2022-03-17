using AutoMapper;
using System;
using UserManagementAPI.Infrastructure.Models;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserRequest, User>()
                .ForMember(dest => dest.ImageName, opt =>
                   opt.MapFrom((src, dest, destMember, ctx) => ctx.Items["defaultImageName"]));

            CreateMap<User, UserListItemDto>();

            CreateMap<User, GetUserByIdResponse>();

            CreateMap<Tuple<string, DateTimeOffset>, GetUserImageUrlResponse>()
                .ForMember(dest => dest.UserId, opt =>
                   opt.MapFrom((src, dest, destMember, ctx) => ctx.Items["userId"]))
                .ForMember(dest => dest.ImageUrl, opt =>
                   opt.MapFrom(src => src.Item1))
                .ForMember(dest => dest.ExpirationDate, opt =>
                   opt.MapFrom(src => src.Item2.UtcDateTime));
        }
    }
}
