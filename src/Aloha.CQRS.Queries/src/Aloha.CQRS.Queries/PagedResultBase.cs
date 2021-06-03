using System;
using System.Collections.Generic;
using System.Text;

namespace Aloha.CQRS.Queries
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }

        public int ResultsPerPage { get; set; }

        public int  TotalPages { get; set; }

        public long TotalResults { get; set; }

        protected PagedResultBase() { } 

        protected PagedResultBase(int currentPage, int resultsPerPage,
           int totalPages, long totalResults)
        {
            CurrentPage = currentPage > totalPages ? totalPages : currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }
}
