using XSchool.Core;
using XSchool.Helpers;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核模板
    /// </summary>
    public class KpiTemplate : IModel<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>
        public KpiPlan KpiId { get; set; } = KpiPlan.Monthly;

        /// <summary>
        /// 所属考核方案
        /// </summary>
        public string KpiName => KpiId.GetDescription();

        /// <summary>
        /// 类型，1-部门，2-人员
        /// </summary>
        public KpiType Type { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }
    }
}

