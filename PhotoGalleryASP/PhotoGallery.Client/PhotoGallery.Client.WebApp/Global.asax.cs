using AutoMapper;
using PhotoGallery.Client.WebApp.App_Start;
using PhotoGallery.Client.WebApp.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PhotoGallery.Client.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AutofacConfig.ConfigureContainer();
            UserAutoMapper.RegisterMappings();
            PhotoAutoMapper.RegisterMappings();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket == null)
                    throw new Exception("Incorrect system behavior");
                var serializer = new JavaScriptSerializer();
                PhotoGalleryPrincipalSerializeModel serializeModel =
                    serializer.Deserialize<PhotoGalleryPrincipalSerializeModel>(authTicket.UserData);
                PhotoGalleryPrincipal principal = new PhotoGalleryPrincipal(authTicket.Name);
                Mapper.Map(serializeModel, principal);
                HttpContext.Current.User = principal;
            }
        }
    }
}
