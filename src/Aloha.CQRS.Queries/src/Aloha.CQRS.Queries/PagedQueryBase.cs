namespace Aloha.CQRS.Queries
{
    class PagedQueryBase : IPagedQuery
    {
        public int Page { get; }
        public int Results { get; }
        public string OrderBy { get; }
        public string SortOrder { get; }
    }
}
