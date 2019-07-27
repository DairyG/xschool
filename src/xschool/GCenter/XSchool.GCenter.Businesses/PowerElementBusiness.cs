using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class PowerElementBusiness : Business<PowerElement>
    {
        private readonly PowerElementRepository _repository;
        public PowerElementBusiness(IServiceProvider provider, PowerElementRepository repository) : base(provider, repository)
        {
            _repository = repository;
        }

        public Result UpdateBatch(List<int> ids)
        {
            return _repository.Update(p => ids.Contains(p.Id) && p.IsSystem == IsSystem.No, p => new PowerElement()
            {
                Status = NomalStatus.Invalid
            }) ? Result.Success() : Result.Fail("操作失败");
        }

    }
}
