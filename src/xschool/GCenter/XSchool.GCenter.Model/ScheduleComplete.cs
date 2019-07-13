using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class ScheduleComplete : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 日程ID
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// 完成人Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
