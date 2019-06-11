using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Core;
using XSchool.Query.Pageing;
using XSchool.Repositories;
using MSLogger = Microsoft.Extensions.Logging.ILogger;

namespace XSchool.Businesses
{
    public class Business<TModel, TKey>
         where TModel : class, IModel<TKey>, new() where TKey : IEquatable<TKey>
    {
        //public ITemplateContent TemplateContent
        //{
        //    get
        //    {
        //        return this.Repository.TemplateContent;
        //    }
        //    set { this.Repository.TemplateContent = value; }
        //}

        protected MSLogger Logger { get; }
        protected Repository<TModel, TKey> Repository { get; }
        protected IServiceProvider ServiceProvider { get; }



        public Business(IServiceProvider provider, Repository<TModel, TKey> repository)
        {
            this.ServiceProvider = provider;
            this.Repository = repository;
            var factory = (ILoggerFactory)provider.GetService(typeof(ILoggerFactory));
            this.Logger = factory.CreateLogger(this.GetType());
        }

        public virtual Result Add(TModel model)
        {
            Repository.Add(model);
            return Result.Success(model.Id);
        }

        public virtual Result AddRange(params TModel[] models)
        {
            this.AddRange(new List<TModel>(models));
            return Result.Success();
        }

        public virtual Result AddRange(IList<TModel> collection)
        {
            this.Repository.AddRange(collection);
            return Result.Success();
        }

        public virtual Result Delete(TKey key)
        {
            this.Repository.Delete(key);
            return Result.Success();
        }

        public virtual Result Delete(params TKey[] keys)
        {
            this.Repository.Delete(keys);
            return Result.Success();
        }


        public virtual Result Delete(Expression<Func<TModel, bool>> where)
        {
            this.Repository.Delete(where);
            return Result.Success();
        }

        public virtual Result Update(TModel model)
        {
            this.Repository.Update(model);
            return Result.Success();
        }


        public virtual TModel GetSingle(TKey key)
        {
            return this.Repository.GetSingle(p => p.Id.Equals(key));
        }

        public virtual Result<TModel> GetSingle(Expression<Func<TModel, bool>> where)
        {
            return this.GetSingle(where, p => p);
        }

        public virtual Result<TResult> GetSingle<TResult>(TKey key, Expression<Func<TModel, TResult>> select)
        {
            return this.GetSingle(p => p.Id.Equals(key), select);
        }

        public virtual Result<TResult> GetSingle<TResult>(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TResult>> select)
        {
            var obj = this.Repository.GetSingle(where, select);
            return Result.Success(obj);
        }

        public virtual Result<IList<TModel>> Query(Expression<Func<TModel, bool>> where)
        {
            return Result.Success(this.Repository.Query(where));
        }




        public virtual Result<IList<TSelect>> Query<TSelect>(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select)
        {
            var list = this.Repository.Query(where, select);
            return Result.Success(list);
        }

        public virtual Result<IList<TModel>> Query()
        {
            var list = this.Repository.Query();
            return Result.Success(list);
        }



        public virtual Result<IList<TSelect>> Query<TSelect>(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select, IEnumerable<KeyValuePair<string, OrderBy>> orderColumns)
        {
            var list = this.Repository.Query(where, select, orderColumns);
            return Result.Success(list);
        }

        public virtual IPageCollection<TModel> Page(int page, int size, Expression<Func<TModel, bool>> where)
        {
            var pageCollection = this.Repository.Page(page, size, where);
            return pageCollection;
        }

        public virtual Result<IPageCollection<TModel>> Page(int page, int size, Expression<Func<TModel, bool>> where,
            IEnumerable<KeyValuePair<string, OrderBy>> orderColumns)
        {
            var pageCollection = this.Repository.Page(page, size, where, orderColumns);
            return Result.Success(pageCollection);
        }

        public virtual IPageCollection<TSelect> Page<TSelect>(int page, int size, Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select)
        {
            var pageCollection = this.Repository.Page(page, size, where, select);
            return pageCollection;
        }

        public virtual Result<IPageCollection<TSelect>> Page<TSelect>(int page, int size, Expression<Func<TModel, bool>> where, Expression<Func<TModel, TSelect>> select
        , IEnumerable<KeyValuePair<string, OrderBy>> orderColumns)
        {
            var pageCollection = this.Repository.Page(page, size, where, select, orderColumns);
            return Result.Success(pageCollection);
        }

        public virtual int Count(Expression<Func<TModel, bool>> where)
        {
            var count = Repository.Count(where);
            return count;
        }

        public virtual bool Exist(TKey key)
        {
            return this.Exist(p => p.Id.Equals(key));
        }

        public virtual bool Exist(Expression<Func<TModel, bool>> where)
        {
            var exist = this.Repository.Exist(where);
            return exist;
        }

        /*
        protected virtual IList<Tree> GetTree<Tree>(int Pid, IList<Tree> lst) where Tree : TreeModel<Tree>
        {
            var root = lst.Where(p => p.Pid == 0).ToList();
            foreach (var node in root)
            {
                Recursive(node, lst);
            }
            return root;
        }

        private void Recursive<Tree>(Tree node, IList<Tree> source) where Tree : TreeModel<Tree>
        {
            foreach (var item in source)
            {
                if (node.Id == item.Pid)
                {
                    node.Children.Add(item);
                    Recursive(item, source);
                }
            }
        }
        */

    }

    public class Business<TModel> : Business<TModel, int>
    where TModel : class, IModel<int>, new()
    {
        public Business(IServiceProvider provider, Repository<TModel, int> repository) : base(provider, repository)
        {

        }
    }
}
