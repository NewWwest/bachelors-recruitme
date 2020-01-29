using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Utilities.Paging
{
    public class PagedResponse<T>
    {
        public int TotalCount { get; set; }

        public int Page { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
