using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class PowerRoleBusiness : Business<PowerRole>
    {
        private readonly PowerRoleRepository _repository;
        public PowerRoleBusiness(IServiceProvider provider, PowerRoleRepository repository) : base(provider, repository)
        {
            _repository = repository;
        }

        public Result UpdateBatch(List<int> ids)
        {
            return _repository.Update(p => ids.Contains(p.Id), p => new PowerRole()
            {
                Status = NomalStatus.Invalid
            }) ? Result.Success() : Result.Fail("操作失败");
        }
    }
}
