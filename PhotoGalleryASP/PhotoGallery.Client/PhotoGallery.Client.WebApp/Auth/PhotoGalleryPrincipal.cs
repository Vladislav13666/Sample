using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PhotoGallery.Client.WebApp.Auth
{
    public class PhotoGalleryPrincipal : IPhotoGalleryPrincipal
    {
        public int Id { get; set; }

        public IIdentity Identity { get; private set; }
      
    

        public PhotoGalleryPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}