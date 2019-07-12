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
            return Result.Success();
        }
    }
}
