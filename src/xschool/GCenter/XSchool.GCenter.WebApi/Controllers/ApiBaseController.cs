using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq.Expressions;

namespace XSchool.GCenter.WebApi.Controllers
{
    //internal class ParameterReplacer : ExpressionVisitor
    //{
    //    private readonly ParameterExpression _parameter;

    //    protected override Expression VisitParameter(ParameterExpression node)
    //    {
    //        return base.VisitParameter(_parameter);
    //    }

    //    internal ParameterReplacer(ParameterExpression parameter)
    //    {
    //        _parameter = parameter;
    //    }
    //}

    //public class Condition<T>
    //{
    //    public Expression Node { get; private set; }

    //    public Condition<T> And(Expression<Func<T, bool>> where)
    //    {
    //        if (Node == null)
    //        {
    //            Node = where.Body;
    //        }
    //        else
    //        {
    //            Node = Expression.AndAlso(Node, where.Body);
    //        }

    //        return this;
    //    }

    //    public Condition<T> Or(Expression<Func<T, bool>> where)
    //    {
    //        if (Node == null)
    //        {
    //            Node = where.Body;
    //        }
    //        else
    //        {
    //            Node = Expression.OrElse(Node, where.Body);
    //        }

    //        return this;
    //    }

    //    public Expression<Func<T, bool>> Combine()
    //    {
    //        if (this.Node != null)
    //        {
    //            var p = Expression.Parameter(typeof(T), "p");
    //            //this.Node = (BinaryExpression)new ParameterReplacer(p).Visit(this.Node);
    //            this.Node = new ParameterReplacer(p).Visit(this.Node);
    //            return Expression.Lambda<Func<T, bool>>(this.Node, p);
    //        }
    //        return null;
    //    }
    //}

    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected Token UToken
        {
            get
            {
                return this.HttpContext.Items["TOKEN_USER"] as Token;
            }
        }
    }

    public class XFilterFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var identity = context.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                var token = new Token();
                token.Id = Convert.ToInt32(identity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier));
                token.UserName = identity.Name;
                context.HttpContext.Items["TOKEN_USER"] = token;

            }
            base.OnActionExecuting(context);
        }
    }

    public class Token
    {
        public int Id { get; set; }

        public string UserName { get; set; }

    }
}
