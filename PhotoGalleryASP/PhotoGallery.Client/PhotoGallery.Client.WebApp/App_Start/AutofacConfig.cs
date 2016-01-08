using Autofac;
using Autofac.Integration.Mvc;
using PhotoGallery.Client.Reference.PhotoGalleryServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Client.WebApp.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            //получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerLifetimeScope();
            // регистрируем споставление типов
            builder.RegisterType<PhotoGalleryServiceClient>().As<IPhotoGalleryService>().InstancePerLifetimeScope();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();
            //AutofacHostFactory.Container = container;
            // установка сопоставителя зависимостей

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}