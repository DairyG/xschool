using System;
using XSchool.Core;


namespace XSchool.GCenter.Model
{
    public class Schedule : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 执行人（用,隔开）
        /// </summary>
        public string Executors { get; set; }
        /// <summary>
        /// 执行人Json(id+名字)
        /// </summary>
        public string ExecutorsJson { get; set; }
        /// <summary>
        /// 抄送人（用,隔开）
        /// </summary>
        public string Scribbles { get; set; }
        /// <summary>
        /// 抄送人Json(id+名字)
        /// </summary>
        public string ScribblesJson { get; set; }
        /// <summary>
        /// 考核方案
        /// </summary>
        public KpiPlan KpiPlan { get; set; }
        /// <summary>
        /// 考核项目ID
        /// </summary>
        public int KpiId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 提醒时间（枚举）
        /// </summary>
        public RemindTime RemindTime { get; set; }
        /// <summary>
        /// 提醒方式（枚举）
        /// </summary>
        public RemindWay RemindWay { get; set; }
        /// <summary>
        /// 紧急程度（枚举）
        /// </summary>
        public Emergency Emergency { get; set; }
        /// <summary>
        /// 重复
        /// </summary>
        public Repeat Repeat { get; set; }
        /// <summary>
        /// 重复结束时间
        /// </summary>
        public DateTime? RepeatEndTime { get; set; }
        /// <summary>
        /// 任务标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DptId { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        ///任务内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 月、周、日（月=“true”，周、日=“false”）
        /// </summary>
        public string AllDay { get; set; }
    }
}
