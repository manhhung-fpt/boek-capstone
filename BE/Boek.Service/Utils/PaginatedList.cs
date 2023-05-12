namespace Boek.Service.Utils
{
    public static class PaginatedList
    {
        public static (int, IQueryable<TResult>) PagingQueryable<TResult>(this IQueryable<TResult> source, int page,
            int size, int limitPaging, int defaultPaging)
        {
            if (size > limitPaging)
            {
                size = limitPaging;
            }

            if (size < 1)
            {
                size = defaultPaging;
            }

            if (page < 1)
            {
                page = 1;
            }

            int total = source.Count();
            IQueryable<TResult> results = source.Skip((page - 1) * size).Take(size);
            return (total, results);
        }
    }
}
