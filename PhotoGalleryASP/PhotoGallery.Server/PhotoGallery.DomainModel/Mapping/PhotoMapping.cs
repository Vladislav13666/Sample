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
   public class PhotoMapping
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Photo, PhotoDto>();
            Mapper.CreateMap<PhotoDto, Photo>();
            Mapper.CreateMap<Photo, PhotoInfoDto>();
        }
    }
}
