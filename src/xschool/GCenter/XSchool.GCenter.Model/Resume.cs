using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 简历
    /// </summary>
    public class Resume : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名拼音简写
        /// </summary>
        public string PinYinName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 性别，[1-男，2-女]
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 个人邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 个人QQ
        /// </summary>
        public string Qq { get; set; }

        /// <summary>
        /// 所属民族
        /// </summary>
        public string Folk { get; set; }

        /// <summary>
        /// 个人籍贯
        /// </summary>
        public string NativePlace { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalStatus { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduateInstitutions { get; set; }

        /// <summary>
        /// 所学专业
        /// </summary>
        public string Professional { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime GraduationDate { get; set; }

        /// <summary>
        /// 身高CM
        /// </summary>
        public int? Stature { get; set; }

        /// <summary>
        /// 体重KG
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string Marriage { get; set; }

        /// <summary>
        /// 有无子女
        /// </summary>
        public string Children { get; set; }

        /// <summary>
        /// 招聘来源
        /// </summary>
        public string RecruitSource { get; set; }

        /// <summary>
        /// 应聘职位
        /// </summary>
        public string JobCandidates { get; set; }

        /// <summary>
        /// 工作年限
        /// </summary>
        public int JobYears { get; set; }

        /// <summary>
        /// 期望月薪
        /// </summary>
        public string ExpectSalary { get; set; }

        /// <summary>
        /// 到岗时间
        /// </summary>
        public string ArrivalTime { get; set; }

        /// <summary>
        /// 身份证省份
        /// </summary>
        public string IdCardProvince { get; set; }

        /// <summary>
        /// 身份证城市
        /// </summary>
        public string IdCardCity { get; set; }

        /// <summary>
        /// 身份证区县
        /// </summary>
        public string IdCardCounty { get; set; }

        /// <summary>
        /// 身份证所在地区
        /// </summary>
        public string IdCardArea { get; set; }

        /// <summary>
        /// 身份证详细地址
        /// </summary>
        public string IdCardAddress { get; set; }

        /// <summary>
        /// 居住省份
        /// </summary>
        public string LiveProvince { get; set; }

        /// <summary>
        /// 居住城市
        /// </summary>
        public string LiveCity { get; set; }

        /// <summary>
        /// 居住区县
        /// </summary>
        public string LiveCounty { get; set; }

        /// <summary>
        /// 居住所在地区
        /// </summary>
        public string LiveArea { get; set; }

        /// <summary>
        /// 居住详细地址
        /// </summary>
        public string LiveAddress { get; set; }

        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string Hobby { get; set; }

        /// <summary>
        /// 个人照片，多个以,分割
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// 证书证件，多个以,分割
        /// </summary>
        public string CertificatePath { get; set; }

        /// <summary>
        /// 家庭成员及主要社会关系，注：以Json格式保存
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// 教育经历，注：以Json格式保存
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 工作经历，注：以Json格式保存
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }

        /// <summary>
        /// 办公邮箱
        /// </summary>
        public string OfficeEmail { get; set; }

        /// <summary>
        /// 传真号码
        /// </summary>
        public string FaxNumber { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public string Referees { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        public string PositionDescribe { get; set; }

        /// <summary>
        /// 面试状态
        /// </summary>
        public InterviewStatus InterviewStatus { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        public ResumeStatus Status { get; set; }
    }
}
