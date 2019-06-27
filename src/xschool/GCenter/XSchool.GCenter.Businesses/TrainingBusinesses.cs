using System;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;


namespace XSchool.GCenter.Businesses
{
    public class TrainingBusinesses : Business<Training>
    {
        public TrainingBusinesses(IServiceProvider provider, TrainingRepository repository) : base(provider, repository) { }

        private Result Check(Training model)
        {
            if (model.PersonId <= 0)
            {
                return Result.Fail("请先填写人员信息");
            }
            if (string.IsNullOrWhiteSpace(model.Course))
            {
                return Result.Fail("培训课程不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Institutions))
            {
                return Result.Fail("培训机构不能为空");
            }
            if (DateTime.Compare(model.EndDate, model.StartDate) < 0)
            {
                return Result.Fail("结束时间不能小于开始时间");
            }
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                return Result.Fail("培训地点不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                return Result.Fail("培训内容不能为空");
            }

            return Result.Success();
        }

        public Result AddOrEdit(Training model)
        {
            var result = Check(model);

            //新增
            if (model.Id <= 0)
            {
                return result.Succeed ? base.Add(model) : result;
            }
            else
            {
                return result.Succeed ? base.Update(model) : result;
            }
        }

    }
}
