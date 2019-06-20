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
        public IList<BasicInfoDto> WorkerInField { get; set; }

        /// <summary>
        /// 面试方式
        /// </summary>
        public IList<BasicInfoDto> InterviewMethod { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public IList<BasicInfoDto> Education { get; set; }

        /// <summary>
        /// 教育性质
        /// </summary>
        public IList<BasicInfoDto> Properties { get; set; }

        /// <summary>
        /// 社会关系
        /// </summary>
        public IList<BasicInfoDto> SocialRelations { get; set; }

        /// <summary>
        /// 招聘来源
        /// </summary>
        public IList<BasicInfoDto> RecruitmentSource { get; set; }

        /// <summary>
        /// 合同性质
        /// </summary>
        public IList<BasicInfoDto> ContractNature { get; set; }

        /// <summary>
        /// 工资类别
        /// </summary>
        public IList<BasicInfoDto> WagesType { get; set; }

        /// <summary>
        /// 保险类别
        /// </summary>
        public IList<BasicInfoDto> InsuranceType { get; set; }
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
