using Academy.Domain.Entities;

namespace Academy.Domain.Pagination
{
    public class PagedResult<T> where T : Entity
    {
        public IList<T> Data { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }
}
