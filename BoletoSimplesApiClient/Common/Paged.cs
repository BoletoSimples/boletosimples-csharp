using System.Collections.Generic;

namespace BoletoSimplesApiClient.Common
{
    /// <summary>
    /// Representa uma resposta paginada 
    /// </summary>
    /// <typeparam name="TResponse">Model referente aos itens que compõem o resultado</typeparam>
    public sealed class Paged<TResponse>
    {
        public int Total { get; private set; }
        public int TotalOfPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int MaxPageSize { get; private set; }
        public List<TResponse> Items { get; private set; }

        public Paged(int total, int totalOfPages, int currentPage, int maxPageSize, List<TResponse> items)
        {
            Total = total;
            TotalOfPages = totalOfPages;
            CurrentPage = currentPage;
            MaxPageSize = maxPageSize;
            Items = items;
        }
    }
}
