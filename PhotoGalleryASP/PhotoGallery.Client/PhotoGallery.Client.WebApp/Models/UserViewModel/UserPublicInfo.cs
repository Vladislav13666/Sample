using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.UserViewModel
{
    public class UserPublicInfo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
    }
}