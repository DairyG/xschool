using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiTemplateRecordBusiness : Business<KpiTemplateRecord>
    {
        public KpiTemplateRecordBusiness(IServiceProvider provider, KpiTemplateRecordRepository repository) : base(provider, repository) { }

    }
}
