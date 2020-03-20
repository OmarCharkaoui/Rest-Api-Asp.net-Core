using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithAPI.Pagination
{
    public class Pagination<T> : List<T>
    {
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious
        {
            get { return PageNumber > 1; }
        }
        public bool HasNext
        {
            get { return TotalPages > PageNumber; }
        }


        public Pagination(List<T> items,int pgSize, int pgNumber, int count )
        {
            TotalCount = count;
            PageSize = pgSize;
            PageNumber = pgNumber;
            TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);
            AddRange(items);
        }

        public static Pagination<T> ToPagingList(IEnumerable<T> source, int pageNumber, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            return new Pagination<T>(items, PageSize, pageNumber, count );

        }

    }
}
