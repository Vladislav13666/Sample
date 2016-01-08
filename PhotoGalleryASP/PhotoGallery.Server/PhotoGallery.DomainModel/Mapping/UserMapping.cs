using AutoMapper;
using PhotoGallery.DomainModel.Entities;
using PhotoGallery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Mapping
{
    public class UserMapping
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<UserRegisterDto, User>();
        }
    }
}
