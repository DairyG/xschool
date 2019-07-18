using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiTemplateBusiness : Business<KpiTemplate>
    {
        public KpiTemplateBusiness(IServiceProvider provider, KpiTemplateRepository repository) : base(provider, repository) { }

    }
}
