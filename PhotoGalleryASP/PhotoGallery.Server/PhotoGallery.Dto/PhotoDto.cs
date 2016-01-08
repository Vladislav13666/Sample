using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Dto
{
    [DataContract]
   public class PhotoDto
    {
        [DataMember]
        public int PhotoId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string PhotoOwner { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public int CurrentUserRating { get; set; }

        [DataMember]
        public int AverageRating { get; set; }
    }
}
