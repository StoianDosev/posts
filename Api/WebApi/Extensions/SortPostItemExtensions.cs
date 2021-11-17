using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Responses;

namespace WebApi.Extensions
{
    public static class SortPostItemExtensions
    {
        public static IEnumerable<PostItem> Sort(this IEnumerable<PostItem> posts, string sortBy, string sortOrder)
        {
            Func<PostItem, object> order = x => x.Id;

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortOrder))
            {
                sortBy = sortBy.ToLower();
                sortOrder = sortOrder.ToLower();

                if (sortBy == "title")
                {
                    order = x => x.Title;
                }
                else if (sortBy == "username")
                {
                    order = x => x.UserName;
                }

                if (sortOrder == "asc")
                {
                    return posts.OrderBy(order);
                }
            }

            return posts.OrderByDescending(order);
        }
    }
}