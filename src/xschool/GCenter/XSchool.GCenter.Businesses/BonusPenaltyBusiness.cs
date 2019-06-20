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
    public class BonusPenaltyBusiness : Business<BonusPenaltySetting>
    {
        private readonly BonusPenaltyRepository _repository;
        public BonusPenaltyBusiness(IServiceProvider provider, BonusPenaltyRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public virtual Result<BonusPenaltySetting> GetSingle(string Name)
        {
            return Result.Success(_repository.GetSingle(p => p.Name == Name));
        }

        public IList<BonusPenaltySetting> Get()
        {
            return base.Query(p => p.WorkinStatus.Equals(EDStatus.Enable));
        }

        public override Result Add(BonusPenaltySetting model)
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

        public override Result Update(BonusPenaltySetting model)
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
            oldModel.Memo = model.Memo;
            oldModel.SortId = model.SortId;
            oldModel.WorkinStatus = model.WorkinStatus;
            oldModel.IsSystem = model.IsSystem;
            oldModel.AddSubtraction = model.AddSubtraction;
            return base.Update(oldModel);
        }

        private Result ChkData(BonusPenaltySetting model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            if (model.IsSystem.Equals(IsSystem.Yes))
            {
                return Result.Fail("数据为系统数据，无法更改！");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("该项不能为空");
            }
            if (model.AddSubtraction == 0)
            {
                return Result.Fail("请选择加减类型");
            }

            if (GetSingle(p => p.Name == model.Name && p.Id != model.Id) != null)
            {
                return Result.Fail("数据已存在，不能再次使用");
            }

            if (string.IsNullOrWhiteSpace(model.Memo))
            {
                model.Memo = "";
            }
            if (model.SortId <= 0)
            {
                model.SortId = 10000;
            }
            model.Name = model.Name.Trim();

            model.Memo = model.Memo.Trim();
            return Result.Success();
        }
    }
}
