using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 通知公告
    /// </summary>
    public class Note : IModel<int>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布人Id
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 公告附件Url地址
        /// </summary>
        public string EnclosureUrl { get; set; }
        /// <summary>
        /// 是否强制阅读
        /// </summary>
        public int IsNeedRead { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
