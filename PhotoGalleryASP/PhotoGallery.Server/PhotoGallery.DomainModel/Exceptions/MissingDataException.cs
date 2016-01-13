using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Exceptions
{
    public class MissingDataException: Exception
    {
        public MissingDataException() : base() { }
        public MissingDataException(string s) : base(s) { }
    }
}
