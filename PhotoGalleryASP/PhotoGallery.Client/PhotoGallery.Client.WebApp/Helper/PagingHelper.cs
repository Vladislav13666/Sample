using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Client.WebApp.Helper
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();
                        
            var prevBuilder = new TagBuilder("a");
            prevBuilder.InnerHtml = "«";
            if (currentPage == 1)
            {
                prevBuilder.MergeAttribute("href", "#");
            }
            else
            {
                prevBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage - 1));
            }
            builder.AppendLine(prevBuilder.ToString());
                      
            for (int i = 1; i <= totalPages; i++)
            {               
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a");
                    subBuilder.InnerHtml = i.ToString();
                    subBuilder.AddCssClass("btn btn-default");
                    if (i == currentPage)
                    {
                        subBuilder.MergeAttribute("href", "#");
                        subBuilder.AddCssClass("selected");
                        subBuilder.AddCssClass("btn-primary");
                    }
                    else
                    {
                        subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                       
                    }
                    builder.AppendLine(subBuilder.ToString());
                }
                else if ((i == 4) && (currentPage > 5))
                {                   
                    builder.AppendLine("<a href=\"#\">...</a>");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {                    
                    builder.AppendLine("<a href=\"#\">...</a>");
                }
            }
          
            var nextBuilder = new TagBuilder("a");
            nextBuilder.InnerHtml = "»";
            if (currentPage == totalPages)
            {
                nextBuilder.MergeAttribute("href", "#");
            }
            else
            {
                nextBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage + 1));
            }
            builder.AppendLine(nextBuilder.ToString());
            return new MvcHtmlString(builder.ToString());
        }
    }
}