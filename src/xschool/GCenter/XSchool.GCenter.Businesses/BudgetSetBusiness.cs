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
    }
}
