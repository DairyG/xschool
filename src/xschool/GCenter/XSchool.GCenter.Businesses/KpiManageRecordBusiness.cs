using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageRecordBusiness : Business<KpiManageRecord>
    {
        public KpiManageRecordBusiness(IServiceProvider provider, KpiManageRecordRepository repository) : base(provider, repository) { }

    }
}
