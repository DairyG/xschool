using System;
using XSchool.Businesses;
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

    }
}
