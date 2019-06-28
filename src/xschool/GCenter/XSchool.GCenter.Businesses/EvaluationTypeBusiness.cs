using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using System.Linq;

namespace XSchool.GCenter.Businesses
{
    public class EvaluationTypeBusiness : Business<EvaluationType>
    {
        private readonly EvaluationTypeRepository _repository;
        public EvaluationTypeBusiness(IServiceProvider provider, EvaluationTypeRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public IList<EvaluationType> Get()
        {
            return base.Query(p => p.Id > 0);
        }

        public override Result Add(EvaluationType model)
        {
            var res = ChkData(model);
            if (!res.Succeed) { return res; }
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            //model.IsAllowDel = true;
            return base.Add(model);
        }
        private Result ChkData(EvaluationType model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("该项不能为空");
            }
            if (GetSingle(p => p.Name == model.Name && p.Id != model.Id) != null)
            {
                return Result.Fail("数据已存在，不能再次使用");
            }
            model.Name = model.Name.Trim();
            return Result.Success();
        }
    }
}
