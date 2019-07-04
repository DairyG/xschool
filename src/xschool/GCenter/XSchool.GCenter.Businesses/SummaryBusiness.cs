using Logistics.Helpers;
using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses
{
    public class SummaryBusiness : Business<Summary>
    {
        private SummaryRepository _resumeRepository;
        public SummaryBusiness(IServiceProvider provider, SummaryRepository repository) : base(provider, repository)
        {
            _resumeRepository = repository;
        }


        public IList<Summary> Get(int type)
        {
            return base.Query(p => p.Type.Equals((SummaryType)type));
        }

        public override Result Add(Summary model)
        {
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            return base.Add(model);
        }

        public override Result Update(Summary model)
        {
            if (model.Id <= 0)
            {
                return Result.Fail("修改操作主键编号必须大于零");
            }
            var oldModel = GetSingle(p => p.Id == model.Id);
            if (oldModel == null)
            {
                return Result.Fail("未找到该条数据，操作失败");
            }
            return base.Update(model);
        }

    }
}
