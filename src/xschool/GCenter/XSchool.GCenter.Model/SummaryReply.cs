using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class SummaryReply : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 报告ID
        /// </summary>
        public int SummaryId { get; set; }
        /// <summary>
        /// 评论人ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 评论人名字
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Reply { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
