using System;
using System.Linq.Expressions;

namespace XSchool.Helpers
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }

    public class Condition<T>
    {
        public Expression Node { get; private set; }

        public Condition<T> And(Expression<Func<T, bool>> where)
        {
            if (Node == null)
            {
                Node = where.Body;
            }
            else
            {
                Node = Expression.AndAlso(Node, where.Body);
            }

            return this;
        }

        public Condition<T> Or(Expression<Func<T, bool>> where)
        {
            if (Node == null)
            {
                Node = where.Body;
            }
            else
            {
                Node = Expression.OrElse(Node, where.Body);
            }

            return this;
        }

        public Expression<Func<T, bool>> Combine()
        {
            if (this.Node != null)
            {
                var p = Expression.Parameter(typeof(T), "p");
                //this.Node = (BinaryExpression)new ParameterReplacer(p).Visit(this.Node);
                this.Node = new ParameterReplacer(p).Visit(this.Node);
                return Expression.Lambda<Func<T, bool>>(this.Node, p);
            }
            return null;
        }
    }
}
