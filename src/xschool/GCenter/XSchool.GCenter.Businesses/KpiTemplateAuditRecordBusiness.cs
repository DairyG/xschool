using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
namespace XSchool.GCenter.Businesses
{
    public class KpiTemplateAuditRecordBusiness : Business<KpiTemplateAuditRecord>
    {
        public KpiTemplateAuditRecordBusiness(IServiceProvider provider, KpiTemplateAuditRecordRepository repository) : base(provider, repository) { }

    }
}
