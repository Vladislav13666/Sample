using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.PhotoViewModel
{
    public class PhotoModel
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public string PhotoOwner { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int AverageRating { get; set; }
        public int CurrentUserRating { get; set; }
        public DateTime CreateTime { get; set; }
        
    }
}