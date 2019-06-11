namespace XSchool.Query.Pageing
{
    public class LinqPageCollection<TModel> : IPageCollection<TModel>
    {
        private PageQueryable<TModel> _queryable;
        public LinqPageCollection(PageQueryable<TModel> queryable)
        {
            _queryable = queryable;
        }

        public int TotalPages { get => _queryable.TotalPages; }

        public int TotalCount { get => _queryable.TotalCount; }

        public int PageIndex { get => _queryable.PageIndex; set => _queryable.PageIndex = value; }

        public int PageSize { get => _queryable.PageSize; set => _queryable.PageSize = value; }

        public bool IsPreviousPage { get => _queryable.IsPreviousPage; }

        public bool IsNextPage { get => _queryable.IsNextPage; }

        public TModel[] Items { get => _queryable.Data; }
    }
}
