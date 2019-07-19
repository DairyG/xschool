using System;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageDetailBusiness : Business<KpiManageDetail>
    {
        private readonly KpiManageDetailRepository _repository;
        public KpiManageDetailBusiness(IServiceProvider provider, KpiManageDetailRepository repository) : base(provider, repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// [列表] 考核内容
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="seach"></param>
        /// <returns></returns>
        public object QueryManageDetail(int page, int limit, KpiEvaluationManageQueryDto seach)
        {
            return _repository.QueryManageDetail(page, limit, seach);
        }
    }
}
