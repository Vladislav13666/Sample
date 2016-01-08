using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PhotoGallery.Client.WebApp.Auth
{
    public interface IPhotoGalleryPrincipal: IPrincipal
    {
        int Id { get; set; }
      
    }
}