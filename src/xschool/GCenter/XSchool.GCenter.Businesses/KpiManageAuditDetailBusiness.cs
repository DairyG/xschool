using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageAuditDetailBusiness : Business<KpiManageAuditDetail>
    {
        public KpiManageAuditDetailBusiness(IServiceProvider provider, KpiManageAuditDetailRepository repository) : base(provider, repository) { }

    }
}
