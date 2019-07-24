using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class PowerElementBusiness : Business<PowerElement>
    {
        public PowerElementBusiness(IServiceProvider provider, PowerElementRepository repository) : base(provider, repository) { }

    }
}
