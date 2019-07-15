using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class ScheduleReply : IModel<int>
    {
        public int Id { get; set;}
        /// <summary>
        /// 日程ID
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// 回复人ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 回复人姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Reply { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
