using Autofac;
using Autofac.Integration.Wcf;
using PhotoGallery.DomainModel;
using PhotoGallery.DomainModel.Data;
using PhotoGallery.DomainModel.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGalleryWcfService.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PhotoGalleryService>().As<PhotoGalleryService>().InstancePerLifetimeScope();
            builder.RegisterType<UserSrv>().As<IUserSrv>().InstancePerLifetimeScope();
            builder.RegisterType<PhotoSrv>().As<IPhotoSrv>().InstancePerLifetimeScope();
            builder.RegisterType<PhotoGalleryDbContext>().As<PhotoGalleryDbContext>().InstancePerLifetimeScope();
            var container = builder.Build();
            AutofacHostFactory.Container = container;

        }
    }
}