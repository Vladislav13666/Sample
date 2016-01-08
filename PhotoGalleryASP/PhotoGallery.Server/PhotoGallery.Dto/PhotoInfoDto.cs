using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Dto
{
    [DataContract]
    public class PhotoInfoDto
    {
        [DataMember]
        [Required]
        public int PhotoId { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(256)]
        [Required]
        public string ImagePath { get; set; }


    }
}
