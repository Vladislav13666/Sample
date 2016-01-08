using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Exceptions
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException(string s) : base(s) { }
    }
}
