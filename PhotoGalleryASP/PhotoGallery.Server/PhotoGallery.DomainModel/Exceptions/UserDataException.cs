using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Exceptions
{
   public class UserDataException: Exception
    {
        public UserDataException() { }
        public UserDataException(string s) : base(s) { }
    }
}
