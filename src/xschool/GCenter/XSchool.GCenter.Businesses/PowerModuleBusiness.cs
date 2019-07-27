using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Businesses
{
    public class PowerModuleBusiness : Business<PowerModule>
    {
        private readonly PowerModuleRepository _repository;
        public PowerModuleBusiness(IServiceProvider provider, PowerModuleRepository repository) : base(provider, repository)
        {
            _repository = repository;
        }

        public IPageCollection<PowerModule> Page(int page, int limit, int pid)
        {
            return _repository.Page(page, limit, pid);
        }

        public Result UpdateBatch(List<int> ids)
        {
            return _repository.Update(p => ids.Contains(p.Id) && p.IsSystem == IsSystem.No, p => new PowerModule()
            {
                Status = NomalStatus.Invalid
            }) ? Result.Success() : Result.Fail("操作失败");
        }

    }
}
