using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiTemplateRecordBusiness : Business<KpiTemplateRecord>
    {
        private readonly KpiTemplateRecordRepository _repository;
        public KpiTemplateRecordBusiness(IServiceProvider provider, KpiTemplateRecordRepository repository) : base(provider, repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// 查询需要生成的数据
        /// </summary>
        /// <param name="kpiId">考核方案</param>
        /// <param name="year">年份</param>
        /// <param name="kpiDate">考核时间</param>
        /// <returns></returns>
        public List<KpiTemplateRecord> QueryByGenerated(KpiPlan kpiId, int year, string kpiDate)
        {
            return _repository.QueryByGenerated(kpiId, year, kpiDate);
        }

    }
}
