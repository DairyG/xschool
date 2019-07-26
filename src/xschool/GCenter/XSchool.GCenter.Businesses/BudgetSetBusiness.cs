using Logistics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses
{
    public class BudgetSetBusiness : Business<BudgetSet>
    {
        private BudgetSetRepository _budgetSetRepository;
        public BudgetSetBusiness(IServiceProvider provider, BudgetSetRepository repository) : base(provider, repository)
        {
            _budgetSetRepository = repository;
        }
        public Result Add(BudgetSet model) {
            if (model.Id == 0)
            {
                model.AddTime = DateTime.Now;
                return base.Add(model);
            }
            else
            {
                return base.Update(model);
            }
        }
        public IList<BudgetSet> Get(int dptId,int year) {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            if (dptId == 0)
            {
                return base.Query(p => p.Id > 0,p=>p,order);
            }
            else
            {
                return base.Query(p => p.DptId.Equals(dptId) && p.Year.Equals(year));
            }
        }
        public BudgetSet GetSingle(int id)
        {
            return base.GetSingle(p => p.Id.Equals(id));
        }
    }
}
