using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiTemplateDetailBusiness : Business<KpiTemplateDetail>
    {
        public KpiTemplateDetailBusiness(IServiceProvider provider, KpiTemplateDetailRepository repository) : base(provider, repository) { }

    }
}
