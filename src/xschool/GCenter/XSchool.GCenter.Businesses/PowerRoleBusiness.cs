using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class PowerRoleBusiness : Business<PowerRole>
    {
        public PowerRoleBusiness(IServiceProvider provider, PowerRoleRepository repository) : base(provider, repository) { }

    }
}
