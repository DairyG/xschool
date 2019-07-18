namespace XSchool.GCenter.Model.ViewModel
{
    /// <summary>
    /// 考核模板审核人
    /// </summary>
    public class KpiTemplateAuditsDto
    {
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
