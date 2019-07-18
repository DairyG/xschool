namespace XSchool.GCenter.Model.ViewModel
{
    public class KpiTemplateContentsDto
    {
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
