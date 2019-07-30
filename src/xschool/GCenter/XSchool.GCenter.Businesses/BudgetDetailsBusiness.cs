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
    public class BudgetDetailsBusiness : Business<BudgetDetails>
    {
        private BudgetDetailsRepository _budgetDetailsRepository;
        public BudgetDetailsBusiness(IServiceProvider provider, BudgetDetailsRepository repository) : base(provider, repository)
        {
            _budgetDetailsRepository = repository;
        }
        public Result Add(List<BudgetDetails> list)
        {
            foreach (BudgetDetails item in list)
            {
                if (item.Id > 0)
                {
                    base.Update(item);
                }
                else
                {
                    base.Add(item);
                }
            }
            return Result.Success();
        }
        public IList<BudgetDetails> Get(int setId)
        {
            return base.Query(p => p.BudgetSetId.Equals(setId));
        }
        public override Result Delete(int id)
        {
            return base.Delete(p => p.Id.Equals(id));
        }
    }
}
