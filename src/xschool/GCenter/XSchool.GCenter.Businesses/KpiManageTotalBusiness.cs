using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageTotalBusiness : Business<KpiManageTotal>
    {
        public KpiManageTotalBusiness(IServiceProvider provider, KpiManageTotalRepository repository) : base(provider, repository) { }

    }
}
