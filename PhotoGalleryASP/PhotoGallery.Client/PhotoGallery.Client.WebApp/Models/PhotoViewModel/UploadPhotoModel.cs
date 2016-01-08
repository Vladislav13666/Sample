using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.PhotoViewModel
{
    public class UploadPhotoModel
    {
        [Required]
        [StringLength(64)]
        [MinLength(4)]
        public string Title { get; set; }
        
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}