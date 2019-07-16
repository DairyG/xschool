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
    public class SummaryBusiness : Business<Summary>
    {
        private SummaryRepository _summaryRepository;
        public SummaryBusiness(IServiceProvider provider, SummaryRepository repository) : base(provider, repository)
        {
            _summaryRepository = repository;
        }


        public IList<Summary> Get(int type)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            return base.Query(p => p.Type == (SummaryType)type, p => p, order);
        }

        public IList<Summary> Get(Expression<Func<Summary, bool>> where)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            return base.Query(where, p => p, order);
        }

        public Summary GetInfo(int id)
        {
            return base.GetSingle(p => p.Id == id);
        }

        public override Result Add(Summary model)
        {
            //if (model.Id != 0)
            //{
            //    return Result.Fail("添加操作主键编号必须为零");
            //}
            //model.AddTime = DateTime.Now;
            //return base.Add(model);

            model.AddTime = DateTime.Now;
            model.IsRead = IsRead.No;
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

        public int UpdateRead(int id)
        {
            return _summaryRepository.UpdateRead(id);
        }
        public Result Delete(int id)
        {
            return base.Delete(id);
        }
        public void UpdateBalance(List<int> channelInt)
        {
            _summaryRepository.UpdateBalance(channelInt);
        }
    }
}
