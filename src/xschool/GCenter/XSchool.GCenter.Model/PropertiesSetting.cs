using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;


namespace XSchool.GCenter.Model
{
    public class PropertiesSetting : IModel<int>
    {
        /// <summary>
        /// 教育性质Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 教育性质
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
