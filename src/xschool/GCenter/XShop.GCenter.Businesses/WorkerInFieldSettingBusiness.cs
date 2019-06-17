using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XShop.GCenter.Model;
using XShop.GCenter.Repositories;

namespace XShop.GCenter.Businesses
{
    public class WorkerInFieldSettingBusiness : Business<WorkerInFieldSetting>
    {
        private readonly WorkerInFieldSettingRepository _repository;
        public WorkerInFieldSettingBusiness(IServiceProvider provider, WorkerInFieldSettingRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public virtual Result<WorkerInFieldSetting> GetSingle(string Name)
        {
            return Result.Success(_repository.GetSingle(p => p.Name == Name));
        }

        public override Result<IList<WorkerInFieldSetting>> Query()
        {
            return base.Query();
        }

        //public override Result<WorkerInFieldSetting> GetSingle(int key)
        //{
        //    return base.GetSingle(p => p.Id.Equals(key));
        //}

        //public override Result Delete(int key)
        //{
        //    var obj = GetSingle(key).Data;
        //    if (!obj.IsAllowDel || obj.IsSys)
        //    {
        //        return Result.Fail("该职位名称已被使用，不允许被删除");
        //    }
        //    return base.Delete(p => p.IsAllowDel == true && p.Id == key);
        //}

        public override Result Add(WorkerInFieldSetting model)
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

        //public override Result Update(Jobs model)
        //{
        //    var res = ChkData(model);
        //    if (!res.Succeed) { return res; }
        //    if (model.Id <= 0)
        //    {
        //        return Result.Fail("修改操作主键编号必须大于零");
        //    }
        //    var oldModel = GetSingle(p => p.Id == model.Id).Data;
        //    if (oldModel == null)
        //    {
        //        return Result.Fail("未找到该条数据，修改失败");
        //    }
        //    if (oldModel.IsSys)
        //    {
        //        return Result.Fail("系统内置数据，无法被编辑");
        //    }
        //    oldModel.Name = model.Name;
        //    oldModel.Memo = model.Memo;
        //    oldModel.SortId = model.SortId;
        //    return base.Update(oldModel);
        //}

        //public Result SetAllowDel(Jobs model)
        //{
        //    if (model == null) { return Result.Fail("数据为空"); }
        //    if (model.Id <= 0) { return Result.Fail("设置时，主键编号必须大于零"); }
        //    return base.Update(model);
        //}
        private Result ChkData(WorkerInFieldSetting model)
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
