using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Client.WebApp.Helper
{
     public class PageableData<T> where T : class
      {
        protected static int ItemPerPageDefault = 1;

        public IEnumerable<T> List { get; set; }

        public int PageNo { get; set; }

        public int CountPage { get; set; }

        public int ItemPerPage { get; set; }

        public int? UserId { get; set; }
        

           public PageableData(IEnumerable<T> enumerableSet, int page, int countPhoto,int itemPerPage, int? userId=null)
        {
            ItemPerPage = itemPerPage;
            PageNo = page;
            UserId = userId;
            var count = countPhoto;            
            CountPage = (int)decimal.Remainder(count, itemPerPage) == 0 ? count / itemPerPage : count / itemPerPage + 1;
            List = enumerableSet;
            
        }
    }
}