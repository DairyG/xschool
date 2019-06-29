using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class ResumeRecord : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 所属简历ID
        /// </summary>
        public int ResumeId { get; set; }
        /// <summary>
        /// 面试方式ID
        /// </summary>
        public int WorkerInFieldId { get; set; }
        /// <summary>
        /// 面试人ID列表（用,隔开）
        /// </summary>
        public string InterviewerIds { get; set; }
        /// <summary>
        /// 面试内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 面试意见
        /// </summary>
        public string Opinion { get; set; }
        /// <summary>
        /// 仪容仪表
        /// </summary>
        public int Appearance { get; set; }
        /// <summary>
        /// 沟通表达
        /// </summary>
        public int Express { get; set; }
        /// <summary>
        /// 专业知识
        /// </summary>
        public int Speciality { get; set; }
        /// <summary>
        /// 亲和力
        /// </summary>
        public int Affinity { get; set; }
        /// <summary>
        /// 逻辑思维
        /// </summary>
        public int Logic { get; set; }
        /// <summary>
        /// 综合得分
        /// </summary>
        public int Socre { get; set; }
        /// <summary>
        /// 面试时间
        /// </summary>
        public DateTime ResumeTime { get; set; }
        /// <summary>
        /// 面试状态
        /// </summary>
        public InterviewStatus InterviewStatus { get; set; }
    }
}
