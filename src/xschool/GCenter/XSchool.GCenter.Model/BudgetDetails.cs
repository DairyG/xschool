using System;
using XSchool.Core;
namespace XSchool.GCenter.Model
{
    public class BudgetDetails : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 预算科目ID
        /// </summary>
        public int BudgetId { get; set; }
        /// <summary>
        /// 预算科目名称
        /// </summary>
        public string BudgetName { get; set; }
        /// <summary>
        /// 预算设置ID
        /// </summary>
        public int BudgetSetId { get; set; }
        /// <summary>
        /// 一月
        /// </summary>
        public Decimal Jan { get; set; }
        /// <summary>
        /// 二月
        /// </summary>
        public Decimal Feb { get; set; }
        /// <summary>
        /// 三月
        /// </summary>
        public Decimal Mar { get; set; }
        /// <summary>
        /// 四月
        /// </summary>
        public Decimal Apr { get; set; }
        /// <summary>
        /// 五月
        /// </summary>
        public Decimal May { get; set; }
        /// <summary>
        /// 六月
        /// </summary>
        public Decimal Jun { get; set; }
        /// <summary>
        /// 七月
        /// </summary>
        public Decimal Jul { get; set; }
        /// <summary>
        /// 八月
        /// </summary>
        public Decimal Aug { get; set; }
        /// <summary>
        /// 九月
        /// </summary>
        public Decimal Sept { get; set; }
        /// <summary>
        /// 十月
        /// </summary>
        public Decimal Oct { get; set; }
        /// <summary>
        /// 十一月
        /// </summary>
        public Decimal Nov { get; set; }
        /// <summary>
        /// 十二月
        /// </summary>
        public Decimal Dec { get; set; }
        /// <summary>
        /// 总计
        /// </summary>
        public Decimal Total { get; set; }
    }
}
