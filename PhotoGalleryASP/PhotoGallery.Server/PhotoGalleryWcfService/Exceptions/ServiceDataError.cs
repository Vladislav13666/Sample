using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PhotoGalleryWcfService.Exceptions
{
    [DataContract]
    public class ServiceDataError
    {
        public ServiceDataError(string message)
        {
            Message = message;
        }        
        [DataMember]
        public string Message { get; set; }
    }
}