using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XSchool.Core
{
    public class OrderCollection : IEnumerable<KeyValuePair<string, OrderBy>>
    {
        private IList<KeyValuePair<string, OrderBy>> _list = new List<KeyValuePair<string, OrderBy>>();
        public void Add(string columnName, OrderBy order)
        {
            this._list.Add(new KeyValuePair<string, OrderBy>(columnName, order));
        }

        public IEnumerator<KeyValuePair<string, OrderBy>> GetEnumerator()
        {
            for (int i = _list.Count; i > 0; i--)
            {
                yield return _list[i - 1];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = _list.Count; i > 0; i--)
            {
                yield return _list[i - 1];
            }
        }
    }

    public class OrderCollection<TModel> : OrderCollection
    {
        public void Add<TProperty>(Expression<Func<TModel, TProperty>> expression, OrderBy order)
        {
            var member = expression.Body as MemberExpression;
            this.Add(member.Member.Name, order);
        }

        public void Asc<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            this.Add(expression, OrderBy.Asc);
        }

        public void Desc<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            this.Add(expression, OrderBy.Desc);
        }
    }
}
