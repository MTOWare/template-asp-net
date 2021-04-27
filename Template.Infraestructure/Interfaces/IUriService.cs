using System;
using Template.Core.QueryFilters;

namespace Template.Infraestructure.Interfaces
{
    public interface IUriService
    {
        Uri GetUserPaginationUri(UserQueryFilter filter, string actionUrl);
    }
}
