using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class BudgetSet : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DptName { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 总预算
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

    }
}
