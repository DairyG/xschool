using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class NoteBusinesses : Business<Model.Note>
    {
        private readonly NoteRepository _repository;
        public NoteBusinesses(IServiceProvider provider, NoteRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public override Result Add(Model.Note model)
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
        private Result CheckData(Model.Note model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            //if (model.IsSystem.Equals(IsSystem.Yes))
            //{
            //    return Result.Fail("数据为系统数据，无法更改！");
            //}
            //if (string.IsNullOrWhiteSpace(model.Name))
            //{
            //    return Result.Fail("该项不能为空");
            //}

            //if (GetSingle(p => p.Name == model.Name && p.Id != model.Id && p.Type == model.Type) != null)
            //{
            //    return Result.Fail("数据已存在，不能再次使用");
            //}

            //if (string.IsNullOrWhiteSpace(model.Memo))
            //{
            //    model.Memo = "";
            //}
            return Result.Success();
        }
    }
}
