using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class PositionSetting : IModel<int>
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
        /// 职责
        /// </summary>
        public string Duty { get; set; }
        /// <summary>
        /// 入职要求
        /// </summary>
        public string Demand { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 启用状态（1-不启用，2-启用）
        /// </summary>
        public EDStatus WorkinStatus { get; set; }
        /// <summary>
        /// 是否为系统数据（枚举-IsSystem）
        /// </summary>
        public IsSystem IsSystem { get; set; }
    }
}
