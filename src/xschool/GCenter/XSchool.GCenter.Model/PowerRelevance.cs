using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 多对多关系集中映射表
    /// </summary>
    public class PowerRelevance : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 第一个表主键Id
        /// </summary>		
        public int FirstId { get; set; }

        /// <summary>
        /// 第二个表主键Id
        /// </summary>		
        public int SecondId { get; set; }

        /// <summary>
        /// 映射标识
        /// </summary>		
        public int Identifiers { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remarks { get; set; }

    }
}
