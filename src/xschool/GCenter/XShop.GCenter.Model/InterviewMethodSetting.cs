using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XShop.GCenter.Model
{
    public class InterviewMethodSetting : IModel<int>
    {
        /// <summary>
        /// 面试方式Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 面试方式
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 启用状态（0-不启用，1-启用）
        /// </summary>
        public int WorkinStatus { get; set; }
    }
}
