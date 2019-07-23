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
    }
}
