using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class WorkerInFieldSetting : IModel<int>
    {
        /// <summary>
        /// 基础数据ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 基础数据
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
        public EDStatus WorkinStatus { get; set; }
        /// <summary>
        /// 类型（枚举-BasicInfoType）
        /// </summary>
        public BasicInfoType Type { get; set; }
        /// <summary>
        /// 是否为系统数据（枚举-IsSystem）
        /// </summary>
        public IsSystem IsSystem { get; set; }
    }
}
