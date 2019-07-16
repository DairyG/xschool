using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class KpiTemplateDetail : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 考核模板记录Id
        /// </summary>		
        public int KpiTemplateRecordId { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>		
        public int DptId { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>		
        public int EmployeeId { get; set; }

        /// <summary>
        /// 考核项目Id
        /// </summary>		
        public int EvaluationId { get; set; }

        /// <summary>
        /// 考核项目名称
        /// </summary>		
        public string EvaluationName { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>		
        public string EvaluationType { get; set; }

        /// <summary>
        /// 权重
        /// </summary>		
        public int Weight { get; set; }

        /// <summary>
        /// 说明
        /// </summary>		
        public string Explain { get; set; }

    }
}
