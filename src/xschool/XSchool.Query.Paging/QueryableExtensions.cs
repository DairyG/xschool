using System.Linq;

namespace XSchool.Query.Pageing
{
    public static class QueryableExtensions
    {
        public static IPageCollection<T> Page<T>(this IQueryable<T> source, int page, int size)
        {
            return new LinqPageCollection<T>(new PageQueryable<T>(source, page, size));
        }
    }
}
