namespace XSchool.Query.Pageing
{
    public interface IPageCollection<TModel>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页显示多少条记录
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        bool IsPreviousPage { get; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        bool IsNextPage { get; }

        TModel[] Items { get; }

    }
}
