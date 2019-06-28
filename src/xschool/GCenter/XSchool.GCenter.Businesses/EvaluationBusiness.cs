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
    public class EvaluationBusiness : Business<Evaluation>
    {
        private readonly EvaluationRepository _repository;
        public EvaluationBusiness(IServiceProvider provider, EvaluationRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public virtual Result<Evaluation> GetSingle(string Name)
        {
            return Result.Success(_repository.GetSingle(p => p.Name == Name));
        }

        public IList<Evaluation> Get()
        {
            return base.Query(p => p.Id > 0);
        }

        public override Result Add(Evaluation model)
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

        public override Result Update(Evaluation model)
        {
            var res = ChkData(model);
            if (!res.Succeed) { return res; }
            if (model.Id <= 0)
            {
                return Result.Fail("修改操作主键编号必须大于零");
            }
            var oldModel = GetSingle(p => p.Id == model.Id);
            if (oldModel == null)
            {
                return Result.Fail("未找到该条数据，操作失败");
            }
            oldModel.Name = model.Name;
            oldModel.Description = model.Description;
            oldModel.Index = model.Index;
            oldModel.Status = model.Status;
            oldModel.EvaluationTypeId = model.EvaluationTypeId;
            return base.Update(oldModel);
        }

        private Result ChkData(Evaluation model)
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

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                model.Description = "";
            }
            if (model.Index < 0)
            {
                model.Index = 0;
            }
            model.Name = model.Name.Trim();

            model.Description = model.Description.Trim();
            return Result.Success();
        }
    }
}
