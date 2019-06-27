using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 成长(培训)管理
    /// </summary>
    public class Training : IModel<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// 培训课程
        /// </summary>
        public string Course { get; set; }

        /// <summary>
        /// 培训机构
        /// </summary>
        public string Institutions { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 培训地点
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 培训内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 所获荣誉
        /// </summary>
        public string Honor { get; set; }

        /// <summary>
        /// 附件，多个以,分割
        /// </summary>
        public string Attachment { get; set; }

    }
}
