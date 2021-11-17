using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Responses;

namespace WebApi.Extensions
{
    public static class PagePostItemExtesion
    {
        public static IEnumerable<PostItem> Page(this IEnumerable<PostItem> posts, int pageIndex, int pageSize)
        {
            return posts.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}