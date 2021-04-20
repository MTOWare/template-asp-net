using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Core.QueryFilters
{
    public class UserQueryFilter
    {
        public DateTime? Date { get; set; }

        public string Email { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
