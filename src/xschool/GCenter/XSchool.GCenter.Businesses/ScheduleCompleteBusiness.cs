using Logistics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;


namespace XSchool.GCenter.Businesses
{
    public class ScheduleCompleteBusiness : Business<ScheduleComplete>
    {
        private ScheduleCompleteRepository _scheduleCompleteRepository;
        public ScheduleCompleteBusiness(IServiceProvider provider, ScheduleCompleteRepository repository) : base(provider, repository)
        {
            _scheduleCompleteRepository = repository;
        }
        public override Result Add(ScheduleComplete model)
        {
            model.AddTime = DateTime.Now;
            //新增
            if (model.Id <= 0)
            {
                return base.Add(model);
            }
            else
            {
                return base.Update(model);
            }
        }
    }
}
