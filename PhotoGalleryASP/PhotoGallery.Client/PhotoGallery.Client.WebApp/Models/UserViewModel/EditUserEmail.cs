﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Models.UserViewModel
{
    public class EditUserEmail
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        

    }
}