using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageAuditRecordBusiness : Business<KpiManageAuditRecord>
    {
        public KpiManageAuditRecordBusiness(IServiceProvider provider, KpiManageAuditRecordRepository repository) : base(provider, repository) { }

    }
}
