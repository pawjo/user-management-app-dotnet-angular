﻿using AutoMapper;
using UserManagementAPI.Infrastructure.Models;
using UserManagementAPI.Logic.Dtos;

namespace UserManagementAPI.Logic.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserRequest, User>();
            //.ForMember(dest => dest.ImageName, opt =>
            //   opt.MapFrom((src, dest, destMember, ctx) => ctx.Items["defaultImageName"]));

            CreateMap<User, UserListItemDto>();

            CreateMap<User, UserDetailsDto>()
                .ForMember(dest => dest.Image, opt =>
                   opt.MapFrom((src, dest, destMember, ctx) => ctx.Items["userImage"]))
                .ForMember(dest => dest.IsDefaultImage, opt =>
                   opt.MapFrom((src, dest, destMember, ctx) => ctx.Items["userImage"] == null));

            CreateMap<UpdateUserRequest, User>();
        }
    }
}
