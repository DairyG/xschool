using System.Collections.Generic;

namespace XSchool.GCenter.Model.ViewModel
{
    /// <summary>
    /// 基础信息 组装
    /// </summary>
    public class BasicInfoResultDto
    {
        /// <summary>
        /// 到岗时间
        /// </summary>
        public IList<BasicInfoDto> WorkerInField { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 面试方式
        /// </summary>
        public IList<BasicInfoDto> InterviewMethod { get; set; } = new List<BasicInfoDto>();
        /// <summary>
        /// 学历
        /// </summary>
        public IList<BasicInfoDto> Education { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 教育性质
        /// </summary>
        public IList<BasicInfoDto> Properties { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 社会关系
        /// </summary>
        public IList<BasicInfoDto> SocialRelations { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 招聘来源
        /// </summary>
        public IList<BasicInfoDto> RecruitmentSource { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 合同性质
        /// </summary>
        public IList<BasicInfoDto> ContractNature { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 工资类别
        /// </summary>
        public IList<BasicInfoDto> WagesType { get; set; } = new List<BasicInfoDto>();

        /// <summary>
        /// 保险类别
        /// </summary>
        public IList<BasicInfoDto> InsuranceType { get; set; } = new List<BasicInfoDto>();
    }

    /// <summary>
    /// 基础信息
    /// </summary>
    public class BasicInfoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
