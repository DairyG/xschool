using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class KpiTemplateAuditRecord : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 考核模板记录Id
        /// </summary>		
        public int KpiTemplateRecordId { get; set; }

        /// <summary>
        /// 当前步骤
        /// </summary>		
        public KpiSteps Steps { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>		
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>		
        public int DptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>		
        public string DptName { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary>		
        public int JobId { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>		
        public string JobName { get; set; }

    }
}
