using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.PhotoViewModel
{
    public class PhotoEditModel
    {
        [Required]
        public int PhotoId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}