using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class RuleRegulationTypeBusiness : Business<Model.RuleRegulationType>
    {
        private readonly RuleRegulationTypeRepository _repository;
        public RuleRegulationTypeBusiness(IServiceProvider provider, RuleRegulationTypeRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public override Result Add(Model.RuleRegulationType model)
        {
            var res = CheckData(model);
            if (!res.Succeed)
            {
                return res;
            }
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            return base.Add(model);
        }
        private Result CheckData(Model.RuleRegulationType model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            return Result.Success();
        }
    }
    public class RuleRegulationBusiness : Business<Model.RuleRegulation>
    {
        private readonly RuleRegulationRepository _repository;
        public RuleRegulationBusiness(IServiceProvider provider, RuleRegulationRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
    }
}
