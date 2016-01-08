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
            Mapper.CreateMap<Photo, PhotoDto>().ForMember("AverageRating",p=>p.MapFrom(c =>c.AllVotes!=0?c.AllRating/c.AllVotes:0));
            Mapper.CreateMap<PhotoDto, Photo>();
            Mapper.CreateMap<Photo, PhotoInfoDto>();
        }
    }
}
