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
            return base.Exist(p=>p.ScheduleId.Equals(model.ScheduleId) && p.EmployeeId.Equals(model.EmployeeId)) ? Result.Fail("您已完成，无需重复操作") : base.Add(model);
        }
        public IList<ScheduleComplete> Get(int scheId)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            return base.Query(p => p.ScheduleId.Equals(scheId),p=>p, order);
        }
    }
}
