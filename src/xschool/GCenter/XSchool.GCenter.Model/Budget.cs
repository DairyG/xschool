using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class Budget : IModel<int>
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 级联（格式：0,1,2,）
        /// </summary>
        public string LevelMap { get; set; }
        /// <summary>
        /// 启用状态（EDStatus 1-不启用，2-启用）
        /// </summary>
        public EDStatus BgStatus { get; set; }
        /// <summary>
        /// 类型（枚举-BudgetType）
        /// </summary>
        public BudgetType Type { get; set; }
        /// <summary>
        /// 是否为系统数据（枚举-IsSystem）
        /// </summary>
        public IsSystem IsSystem { get; set; }

        /// <summary>
        /// 是否为子节点
        /// </summary>
        [NotMapped]
        public bool IsChild { get; set; }
    }
}
