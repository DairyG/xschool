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
    public class ResumeRecordBusiness : Business<ResumeRecord>
    {
        private readonly ResumeRecordRepository _repository;
        public ResumeRecordBusiness(IServiceProvider provider, ResumeRecordRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }

        public IList<ResumeRecord> Get()
        {
            return base.Query(p => p.Id > 0);
        }

        public override Result Add(ResumeRecord model)
        {
            var res = ChkData(model);
            if (!res.Succeed) { return res; }
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            model.ResumeTime = DateTime.Now;
            return base.Add(model);
        }

        private Result ChkData(ResumeRecord model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            if (model.WorkerInFieldId == 0)
            {
                return Result.Fail("该项不能为空");
            }

            if (string.IsNullOrWhiteSpace(model.InterviewerIds))
            {
                return Result.Fail("面试人不能为空");
            }

            if (string.IsNullOrWhiteSpace(model.Content))
            {
                return Result.Fail("面试内容不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Opinion))
            {
                return Result.Fail("面试意见不能为空");
            }
            if (model.Appearance <= 0)
            {
                model.Appearance = 1;
            }
            if (model.Express <= 0)
            {
                model.Express = 1;
            }
            if (model.Speciality <= 0)
            {
                model.Speciality = 1;
            }
            if (model.Affinity <= 0)
            {
                model.Affinity = 1;
            }
            if (model.Logic <= 0)
            {
                model.Logic = 1;
            }
            if (model.Socre <= 0)
            {
                model.Socre = 1;
            }
            model.Content = model.Content.Trim();
            model.Opinion = model.Opinion.Trim();
            return Result.Success();
        }
    }
}
