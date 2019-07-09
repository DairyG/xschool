using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class Summary : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DptId { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string SummaryDate { get; set; }
        /// <summary>
        /// 时间段（周报【第一周，第二周...】；季度报【第一季度，第二季度...】；年报【上半年，下半年】）
        /// </summary>
        public SummaryIndex Index { get; set; }
        /// <summary>
        /// 报告的类型
        /// </summary>
        public SummaryType Type { get; set; }
        /// <summary>
        /// 完成的工作
        /// </summary>
        public string Finish { get; set; }
        /// <summary>
        /// 工作总结
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 计划/未完成的工作
        /// </summary>
        public string Plan { get; set; }
        /// <summary>
        /// 需协调和帮助
        /// </summary>
        public string Help { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public IsRead IsRead { get; set; }
    }
}
