using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageDetailBusiness : Business<KpiManageDetail>
    {
        public KpiManageDetailBusiness(IServiceProvider provider, KpiManageDetailRepository repository) : base(provider, repository) { }

    }
}
