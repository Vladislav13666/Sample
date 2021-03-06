﻿using AutoMapper;
using PhotoGallery.Client.Reference.PhotoGalleryServ;
using PhotoGallery.Client.WebApp.Models.PhotoViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Mapping
{
    public class PhotoMapping
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<PhotoDto, PhotoModel>();
            Mapper.CreateMap<PhotoModel, PhotoDto>();
            Mapper.CreateMap<PhotoInfoDto, PhotoEditModel>();
            Mapper.CreateMap<PhotoInfoDto, PhotoDeleteModel>();
        }
    }
}