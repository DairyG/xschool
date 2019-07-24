using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class PowerModuleBusiness : Business<PowerModule>
    {
        public PowerModuleBusiness(IServiceProvider provider, PowerModuleRepository repository) : base(provider, repository) { }

    }
}
