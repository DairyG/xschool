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
    public class ScheduleReplyBusiness : Business<ScheduleReply>
    {
        private ScheduleReplyRepository _scheduleReplyRepository;
        public ScheduleReplyBusiness(IServiceProvider provider, ScheduleReplyRepository repository) : base(provider, repository)
        {
            _scheduleReplyRepository = repository;
        }
        public override Result Add(ScheduleReply model)
        {
            model.AddTime = DateTime.Now;
            //新增
            return base.Add(model);
        }
        public IList<ScheduleReply> Get(int scheId)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            return base.Query(p => p.ScheduleId.Equals(scheId), p => p, order);
        }
    }
}
