using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Dto
{
    [DataContract]
    public class UserInfoDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string FullName { get; set; }
    }
}
