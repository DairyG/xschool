using Logistics.Helpers;
using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Businesses
{
    public class ResumeBusiness : Business<Resume>
    {
        private ResumeRepository _resumeRepository;
        public ResumeBusiness(IServiceProvider provider, ResumeRepository repository) : base(provider, repository) {
            _resumeRepository = repository;
        }
        public Result Check(Resume model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                return Result.Fail("个人姓名不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.LinkPhone))
            {
                return Result.Fail("联系电话不能为空");
            }
            if (!RegexHelper.IsMobilePhone(model.LinkPhone))
            {
                return Result.Fail("联系电话格式错误");
            }
            if (!string.IsNullOrWhiteSpace(model.Email) && !RegexHelper.IsEmail(model.Email))
            {
                return Result.Fail("个人邮箱格式错误");
            }
            if (!string.IsNullOrWhiteSpace(model.Qq) && !RegexHelper.IsPositiveInteger(model.Qq))
            {
                return Result.Fail("个人QQ只能为数字");
            }
            if (string.IsNullOrWhiteSpace(model.Folk))
            {
                return Result.Fail("所属民族不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.NativePlace))
            {
                return Result.Fail("个人籍贯不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.PoliticalStatus))
            {
                return Result.Fail("政治面貌不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.IdCard))
            {
                return Result.Fail("身份证号不能为空");
            }
            if (!RegexHelper.IsIdCard(model.IdCard))
            {
                return Result.Fail("请输入正确的身份证号码");
            }
            if (string.IsNullOrWhiteSpace(model.GraduateInstitutions))
            {
                return Result.Fail("毕业院校不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Professional))
            {
                return Result.Fail("所学专业不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Degree))
            {
                return Result.Fail("最高学历不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.JobCandidates))
            {
                return Result.Fail("应聘职位不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.ExpectSalary))
            {
                return Result.Fail("期望月薪（税前）不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.ArrivalTime))
            {
                return Result.Fail("到岗时间不能为空");
            }

            if (model.Id <= 0)
            {
                if (base.Exist(p => p.LinkPhone == model.LinkPhone))
                {
                    return Result.Fail("联系电话已存在");
                }
                if (base.Exist(p => p.IdCard == model.IdCard))
                {
                    return Result.Fail("身份证号已存在");
                }
            }
            else
            {
                if (base.Exist(p => p.LinkPhone == model.LinkPhone && p.Id != model.Id))
                {
                    return Result.Fail("联系电话已存在");
                }
                if (base.Exist(p => p.IdCard == model.IdCard && p.Id != model.Id))
                {
                    return Result.Fail("身份证号已存在");
                }
            }

            return Result.Success();
        }

        public Result AddOrEdit(Resume model)
        {
            var result = Check(model);
            model.PinYinName = PingYinHelper.GetFirstSpell(model.UserName);
            //新增
            if (model.Id <= 0)
            {
                return result.Succeed ? base.Add(model) : result;
            }
            else
            {
                return result.Succeed ? base.Update(model) : result;
            }
        }

        public int Delete(Resume model, int id)
        {
            return _resumeRepository.Delete(model, id);
        }

        /// <summary>
        /// 根据面试状态查询简历
        /// </summary>
        /// <param name="state">面试状态</param>
        /// <returns></returns>
        public IPageCollection<Resume> GetListByInterviewStatus(int page, int limit, InterviewStatus state)
        {
            return _resumeRepository.GetListByInterviewStatus(page, limit, state);
        }
    }
}
