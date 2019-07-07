using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using XSchool.Core;
using XSchool.Query.Pageing;
using Z.EntityFramework.Plus;

namespace XSchool.Repositories
{
    public class Repository<TModel, TKey> where TModel : class, IModel<TKey>, new() where TKey : IEquatable<TKey>
    {
        //public ITemplateContent TemplateContent { get; set; }
        protected DbContext DbContext { get; private set; }
        private readonly DbSet<TModel> _set;
        public Repository(DbContext dbContext)
        {
            //this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            if (dbContext != null)
            {
                this.DbContext = dbContext;
                //this.DbContext.Database.EnsureCreated();
                this._set = this.DbContext.Set<TModel>();
            }
        }

        public virtual int Add(TModel model)
        {
            this._set.Add(model);
            return this.DbContext.SaveChanges();
        }

        public virtual int AddRange(params TModel[] models)
        {
            this.AddRange(new List<TModel>(models));
            return this.DbContext.SaveChanges();
        }

        public virtual int AddRange(IList<TModel> collection)
        {
            this._set.AddRange(collection);
            return this.DbContext.SaveChanges();
        }

        public virtual int Delete(TKey key)
        {
            return Delete(p => p.Id.Equals(key));
            //var query = this._set.Where(p => p.Id.Equals(key));
            //_set.RemoveRange(query);
            //return this.DbContext.SaveChanges();
        }

        public virtual int Delete(params TKey[] keys)
        {

            var query = this._set.Where(p => keys.Contains(p.Id));
            _set.RemoveRange(query);
            return this.DbContext.SaveChanges();
        }


        public virtual int Delete(Expression<Func<TModel, bool>> where)
        {
            var query = this._set.Where(where);
            this._set.RemoveRange(query);
            return this.DbContext.SaveChanges();
        }

        public virtual int Update(TModel model)
        {
            this._set.Update(model);
            return this.DbContext.SaveChanges();
        }

        public virtual int UpdateRange(IEnumerable<TModel> models)
        {
            this._set.UpdateRange(models);
            return this.DbContext.SaveChanges();
        }


        public virtual int Update<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty value, TKey key)
        {
            TModel model = new TModel { Id = key };
            var property = (expression.Body as MemberExpression).Member as PropertyInfo;
            property.SetValue(model, value);
            this.DbContext.Attach(model);
            this.DbContext.Entry(model).Property(expression).IsModified = true;
            return this.DbContext.SaveChanges();
        }
        /// <summary>
        /// 根据Linq更新实体
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="modelsExpression"></param>
        /// <returns></returns>
        public virtual bool Update(Expression<Func<TModel, bool>> filterExpression, Expression<Func<TModel, TModel>> modelsExpression)
        {
            return this.Entites.Where(filterExpression).Update(modelsExpression)>0?true:false;

        }
        /// <summary>
        /// 数据操作实体
        /// </summary>
        public IQueryable<TModel> Entites => this.DbContext.Set<TModel>();

        public virtual TModel GetSingle(TKey key)
        {
            return this.GetSingle(p => p.Id.Equals(key), p => p);
        }

        public virtual TModel GetSingle(Expression<Func<TModel, bool>> where)
        {
            return this.GetSingle(where, p => p);
        }

        public virtual TResult GetSingle<TResult>(TKey key, Expression<Func<TModel, TResult>> select)
        {
            return this.GetSingle(p => p.Id.Equals(key), select);
        }

        public virtual TResult GetSingle<TResult>(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TResult>> select)
        {
            return this._set.Where(where).Select(select).FirstOrDefault();
        }

        public virtual IList<TModel> Query()
        {
            return this._set.ToList();
        }

        public virtual IList<TModel> Query(Expression<Func<TModel, bool>> where)
        {
            return this.Query(where, p => p);
        }

        public virtual IList<TSelect> Query<TSelect>(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select)
        {
            var query = (IQueryable<TModel>)this._set;
            if (where != null)
            {
                query = query.Where(where);
            }
            return query.Select(select).ToList();
        }

        public virtual IList<TSelect> Query<TSelect>(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select, IEnumerable<KeyValuePair<string, OrderBy>> orderColumns)
        {
            var query = (IQueryable<TModel>)this._set;
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderColumns != null)
            {
                query = this.OrderBy(query, orderColumns);
            }
            return query.Select(select).ToList();
        }

        public virtual IPageCollection<TModel> Page(int page, int size)
        {
            //return this._set.Page(page, size);
            return null;
        }

        public IPageCollection<TSelect> Page<TSelect>(int page, int size, Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select)
        {
            return this.Page(page, size, where, select, orderColumns: null);
        }

        public virtual IPageCollection<TModel> Page(int page, int size, Expression<Func<TModel, bool>> where, IEnumerable<KeyValuePair<string, OrderBy>> orderColumns)
        {
            return this.Page(page, size, where, p => p, orderColumns);
        }


        public virtual IPageCollection<TModel> Page(int page, int size, Expression<Func<TModel, bool>> where)
        {
            return this.Page(page, size, where, orderColumns: null);
        }


        public virtual IPageCollection<TSelect> Page<TSelect>(int page, int size, Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select,
            IEnumerable<KeyValuePair<string, OrderBy>> orderColumns)
        {
            var query = (IQueryable<TModel>)this._set;
            if (where != null)
            {
                query = query.Where(where);
            }

            return this.OrderBy(query, orderColumns).Select(select).Page(page, size);
        }

        protected IQueryable<T> OrderBy<T>(IQueryable<T> query, IEnumerable<KeyValuePair<string, OrderBy>> columns)
        {
            if (columns == null) return query;
            IOrderedQueryable<T> orderQuery = null;
            foreach (var kv in columns)
            {
                var columnName = kv.Key;
                var member = typeof(TModel).GetProperty(columnName);
                if (member == null) throw new Exception($"找不到制定的排序列[{columnName}]");
                var p = Expression.Parameter(typeof(TModel), "p");
                var order = kv.Value == Core.OrderBy.Asc ? "OrderBy" : "OrderByDescending";
                var express = Expression.Lambda(Expression.MakeMemberAccess(p, member), p);
                var method = typeof(Queryable).GetMethods()
                    .Single(m => m.Name == order && m.IsGenericMethod && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TModel), member.PropertyType);
                orderQuery = (IOrderedQueryable<T>)method.Invoke(null, new object[] { orderQuery ?? query, express });
            }
            return orderQuery ?? query;
        }



        public virtual int Count(Expression<Func<TModel, bool>> where)
        {
            return this._set.Count(where);
        }

        public virtual bool Exist(TKey key)
        {
            return this.Exist(p => p.Id.Equals(key));
        }

        public virtual bool Exist(Expression<Func<TModel, bool>> where)
        {
            return this._set.Where(where).Any();
        }
    }

    public class Repository<TModel> : Repository<TModel, int> where TModel : class, IModel<int>, new()
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
