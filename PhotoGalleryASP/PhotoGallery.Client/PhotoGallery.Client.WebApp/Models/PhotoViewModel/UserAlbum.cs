using PhotoGallery.Client.WebApp.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.PhotoViewModel
{
    public class UserAlbum
    {     
        public UserPublicInfo UserPublicInfo { get; set; }
        public IEnumerable<PhotoModel> Photos { get; set; }

        public UserAlbum(UserPublicInfo userPublicInfo, IEnumerable<PhotoModel> photos)
        {
            UserPublicInfo = userPublicInfo;
            Photos = photos;
        }
    }
}