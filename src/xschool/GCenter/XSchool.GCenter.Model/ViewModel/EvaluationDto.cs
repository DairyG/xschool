namespace XSchool.GCenter.Model.ViewModel
{
    public class EvaluationDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 考核项目
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 考核项目分类Id
        /// </summary>
        public int EvaluationTypeId { get; set; }
        /// <summary>
        /// 考核项目分类名称
        /// </summary>
        public string EvaluationTypeName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }
    }
}
