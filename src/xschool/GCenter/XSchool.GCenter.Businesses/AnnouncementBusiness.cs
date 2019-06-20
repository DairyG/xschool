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
    public class AnnouncementBusiness : Business<Announcement>
    {
        private readonly AnnouncementRepository _repository;
        public AnnouncementBusiness(IServiceProvider provider, AnnouncementRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public virtual Result<Announcement> GetSingle(string title)
        {
            return Result.Success(_repository.GetSingle(p => p.Title == title));
        }

        public IList<Announcement> Get()
        {
            return base.Query(p => p.AcStatus.Equals(EDStatus.Enable));
        }

        public override Result Add(Announcement model)
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

        public override Result Update(Announcement model)
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
            
            oldModel.IsSystem = model.IsSystem;
            return base.Update(oldModel);
        }

        private Result ChkData(Announcement model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            if (model.IsSystem.Equals(IsSystem.Yes))
            {
                return Result.Fail("数据为系统数据，无法更改！");
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return Result.Fail("该项不能为空");
            }

            if (GetSingle(p => p.Title == model.Title && p.Id != model.Id) != null)
            {
                return Result.Fail("数据已存在，不能再次使用");
            }

            return Result.Success();
        }
    }
}
