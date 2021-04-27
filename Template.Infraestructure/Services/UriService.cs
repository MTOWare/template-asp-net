using Template.Core.QueryFilters;
using Template.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Infraestructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetUserPaginationUri(UserQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

    }
}
