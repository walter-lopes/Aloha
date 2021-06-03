namespace Aloha.CQRS.Queries
{
    public interface IPagedQuery : IQuery
    {
        public int Page { get; }

        public int Results { get; }

        public string OrderBy { get; }

        public string  SortOrder { get; }
    }
}
