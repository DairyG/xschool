using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 模块表
    /// </summary>
    public class PowerModule : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 节点语义Id
        /// </summary>		
        public string LevelMap { get; set; }

        /// <summary>
        /// 路径
        /// </summary>		
        public string Url { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>		
        public int Pid { get; set; }

        /// <summary>
        /// 节点图标
        /// </summary>		
        public string IconName { get; set; }

        /// <summary>
        /// 节点级别
        /// </summary>		
        public int Level { get; set; }

        /// <summary>
        /// 状态
        /// </summary>		
        public NomalStatus Status { get; set; }

        /// <summary>
        /// 是否为系统数据
        /// </summary>		
        public IsSystem IsSystem { get; set; }

        /// <summary>
        /// 排序，数字越小越靠前
        /// </summary>		
        public int DisplayOrder { get; set; }

    }
}
