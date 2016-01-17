using AutoMapper;
using PhotoGallery.Client.Reference.PhotoGalleryServ;
using PhotoGallery.Client.WebApp.Auth;
using PhotoGallery.Client.WebApp.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Mapping
{
    public class UserMapping
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<RegisterUserModel, UserRegisterDto>();
            Mapper.CreateMap<UserDto, PhotoGalleryPrincipalSerializeModel>();
            Mapper.CreateMap<PhotoGalleryPrincipalSerializeModel, PhotoGalleryPrincipal>();
            Mapper.CreateMap<UserDto, EditUserInfo>();
            Mapper.CreateMap<UserDto, EditUserPassword>();
            Mapper.CreateMap<UserInfoDto, UserPublicInfo>();
            Mapper.CreateMap<UserDto, EditUserEmail>();
           
        }
    }
}