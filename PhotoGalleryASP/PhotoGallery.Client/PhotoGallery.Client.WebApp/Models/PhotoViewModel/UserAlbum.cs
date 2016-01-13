using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.PhotoViewModel
{
    public class UserAlbum
    {
        public int? OwnerId { get; set; }
        public IEnumerable<PhotoModel> Photos { get; set; }

        public UserAlbum(IEnumerable<PhotoModel> photos,int? userId=null) {
            OwnerId = userId;
            Photos = photos;
        }
    }
}