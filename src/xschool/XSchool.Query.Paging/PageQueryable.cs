using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace XSchool.Query.Pageing
{
    public class PageQueryable<TModel> : IQueryable<TModel>
    {
        private IQueryable<TModel> _source;

        public PageQueryable(IQueryable<TModel> source, int page, int size)
        {
            if (source != null)
            {
                this._source = source;
                int total = source.Count();

                this.TotalCount = total;
                this.TotalPages = total / size;
                if (total % size > 0) TotalPages++;
                this.PageSize = size;
                this.PageIndex = page;
                this._source = source.Skip((page - 1) * size).Take(size);
                this.Data = _source.ToArray();
            }

        }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsPreviousPage { get { return (PageIndex > 0); } }

        public bool IsNextPage { get { return (PageIndex * PageSize) <= TotalCount; } }

        public Type ElementType => this._source.ElementType;

        public Expression Expression => this._source.Expression;

        public IQueryProvider Provider => this._source.Provider;

        public TModel[] Data { get; }

        Expression IQueryable.Expression => this._source.Expression;

        public IEnumerator<TModel> GetEnumerator()
        {
            foreach (var item in this.Data)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in this.Data)
            {
                yield return item;
            }
        }

    }
}
