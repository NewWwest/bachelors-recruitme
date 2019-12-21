using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Utilities.Paging
{
    public class PagingParameters
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortBy { get; set; }

        public bool? SortDesc { get; set; }
    }
}
