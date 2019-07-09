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
    public class SummaryReplyBusiness : Business<SummaryReply>
    {
        private SummaryReplyRepository _summaryReplyRepository;
        public SummaryReplyBusiness(IServiceProvider provider, SummaryReplyRepository repository) : base(provider, repository)
        {
            _summaryReplyRepository = repository;
        }
        public IList<SummaryReply> Get(int summaryId)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            return base.Query(p => p.SummaryId == summaryId, p => p, order);
        }

        public override Result Add(SummaryReply model)
        {
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            model.AddTime = DateTime.Now;
            return base.Add(model);
        }
    }
}
