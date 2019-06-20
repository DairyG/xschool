using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class Announcement : IModel<int>
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否强制阅读（1-强制阅读 2-不强制阅读）
        /// </summary>
        public int IsCompulsory { get; set; }
        /// <summary>
        /// 能查看的公司ID
        /// </summary>
        public string CompanyIds { get; set; }
        /// <summary>
        /// 能查看的部门ID
        /// </summary>
        public string DepartmentIds { get; set; }
        /// <summary>
        /// 能查看的人员ID
        /// </summary>
        public string PersonIds { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ReadCount { get; set; }
        /// <summary>
        /// 启用状态（1-不启用，2-启用）
        /// </summary>
        public EDStatus AcStatus { get; set; }
        /// <summary>
        /// 是否为系统数据（枚举-IsSystem）
        /// </summary>
        public IsSystem IsSystem { get; set; }
    }
}
