using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
	/// <summary>
	/// 考核管理审核
	/// </summary>
	public class KpiManageAudit : IModel<int>
    {
		public int Id { get; set; }

        /// <summary>
        /// 考核管理记录Id
        /// </summary>
        public int KpiManageRecordId { get; set; }

        /// <summary>
        /// 考核对象类型，1-部门负责人，2-人员
        /// </summary>
        public KpiObjectType ObjectType { get; set; }

        /// <summary>
        /// 初审人Id
        /// </summary>
        public int OneAuditId { get; set; }

        /// <summary>
        /// 终审Id
        /// </summary>
        public int TwoAuditId { get; set; }


    }
}

