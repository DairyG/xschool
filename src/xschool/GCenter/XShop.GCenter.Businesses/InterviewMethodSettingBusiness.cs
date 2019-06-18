using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories;

namespace XShop.GCenter.Businesses
{
    public class InterviewMethodSettingBusiness : Business<InterviewMethodSetting>
    {
        private readonly InterviewMethodSettingRepository _repository;
        public InterviewMethodSettingBusiness(IServiceProvider provider, InterviewMethodSettingRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }

        public virtual Result<InterviewMethodSetting> GetSingle(string Name)
        {
            return Result.Success(_repository.GetSingle(p => p.Name == Name));
        }

        public IList<InterviewMethodSetting> Get()
        {
            return base.Query(p => p.WorkinStatus == 1);
        }

        public override Result Add(InterviewMethodSetting model)
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

        public override Result Update(InterviewMethodSetting model)
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
            return base.Update(oldModel);
        }

        private Result ChkData(InterviewMethodSetting model)
        {
            if (model == null)
            {
                return Result.Fail("到岗时间数据不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("到岗时间不能为空");
            }

            if (GetSingle(p => p.Name == model.Name && p.Id != model.Id) != null)
            {
                return Result.Fail("到岗时间已存在，不能再次使用");
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

