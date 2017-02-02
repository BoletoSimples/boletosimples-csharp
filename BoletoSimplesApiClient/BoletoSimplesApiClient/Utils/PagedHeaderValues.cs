using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace BoletoSimplesApiClient.Utils
{
    /// <summary>
    /// Classe auxiliar para obter dados de paginação do heder de um HttpResponseMessage, se fez necessária porque paginação está ho header e precisou ser parseada 
    /// para que seus dados possam ser transferidos para properties
    /// </summary>
    internal sealed class PagedHeaderValues
    {
        public int Total { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int MaxPageSize { get; private set; }

        // Como a paginação vem no header é necessário essa expressão para identificala e extrair
        private readonly Regex _regex = new Regex("(page=.+&)|(per_page=.+>)", RegexOptions.Compiled, TimeSpan.FromSeconds(2));

        public static PagedHeaderValues ParseFromHttpMessage(HttpResponseMessage response) => new PagedHeaderValues(response);

        private PagedHeaderValues(HttpResponseMessage response)
        {
            var leftQueryStringInfo = ParseQueryStringPagedValues(GetLastPageHeaderLink(response));
            var totalPages = leftQueryStringInfo.Where(d => d.Key == "page").Select(d => d.Value).SingleOrDefault();
            var maxPageSize = leftQueryStringInfo.Where(d => d.Key == "per_page").Select(d => d.Value).SingleOrDefault();

            var rightQueryStringInfo = ParseQueryStringPagedValues(GetNextPageHeaderLink(response));
            var currrentPage = leftQueryStringInfo.Where(d => d.Key == "page").Select(d => d.Value).SingleOrDefault();
            currrentPage = currrentPage > 0 ? currrentPage - 1 : 0;

            Total = int.Parse(response.Headers.GetValues(nameof(Total)).SingleOrDefault());
            TotalPages = totalPages;
            CurrentPage = currrentPage;
            MaxPageSize = maxPageSize;
        }

        private static string GetLastPageHeaderLink(HttpResponseMessage response)
        {
            var links = response.Headers.GetValues("Link").SingleOrDefault();
            var separatorIndex = links.IndexOf(",", 0, links.Length, StringComparison.InvariantCultureIgnoreCase);
            return links.Substring(0, separatorIndex - 1);
        }

        private static string GetNextPageHeaderLink(HttpResponseMessage response)
        {
            var links = response.Headers.GetValues("Link").SingleOrDefault();
            var separatorIndex = links.IndexOf(",", 0, links.Length, StringComparison.InvariantCultureIgnoreCase);
            return links.Substring(separatorIndex + 1, links.Length - (links.Length - separatorIndex + 1));
        }

        private Dictionary<string, int> ParseQueryStringPagedValues(string url)
        {
            var matches = _regex.Matches(url);
            var result = new Dictionary<string, int>();

            foreach (Match item in matches)
            {
                var values = item.Value.Split('=');

                if (values.Any())
                {
                    var key = values.First();
                    var value = int.Parse(values.Last().Replace("&", "").Replace(">", ""));

                    result.Add(key, value);
                }
            }

            return result;
        }
    }
}
