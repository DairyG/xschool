using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核对象
    /// </summary>
	public class KpiTemplateObject : IModel<int>
    {
		/// <summary>
		/// 
		/// </summary>
		public int Id { get; set; }

        /// <summary>
        /// 考核模板Id
        /// </summary>
        public int KpiTemplateId { get; set; }

        /// <summary>
        /// 人员Id/部门Id
        /// </summary>
        public int RelationalId { get; set; }

    }
}

