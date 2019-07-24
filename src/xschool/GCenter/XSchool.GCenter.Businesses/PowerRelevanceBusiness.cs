using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class PowerRelevanceBusiness : Business<PowerRelevance>
    {
        public PowerRelevanceBusiness(IServiceProvider provider, PowerRelevanceRepository repository) : base(provider, repository) { }

    }
}
