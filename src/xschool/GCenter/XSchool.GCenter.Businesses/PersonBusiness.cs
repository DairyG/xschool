using Logistics.Helpers;
using System;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses
{
    public class PersonBusiness : Business<Person>
    {
        private readonly PersonRepository _repository;
        public PersonBusiness(IServiceProvider provider, PersonRepository repository) : base(provider, repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 验证员工 基础信息
        /// </summary>
        public Result CheckBasic(Person model)
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

        /// <summary>
        /// 验证员工 职位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result CheckPosition(Person model)
        {
            if (model.Id <= 0)
            {
                return Result.Fail("请先填写个人信息");
            }
            if (model.DepartmentId == 0)
            {
                return Result.Fail("请选择部门");
            }
            if (model.PositionId == 0)
            {
                return Result.Fail("请选择职位");
            }
            if (string.IsNullOrWhiteSpace(model.EmployeeNo))
            {
                return Result.Fail("请输入员工工号");
            }
            if (model.Status == PersonStatus.Unknown)
            {
                return Result.Fail("请选择在职状态");
            }

            if (base.Exist(p => p.EmployeeNo == model.EmployeeNo && p.Id != model.Id))
            {
                return Result.Fail("员工工号已存在");
            }

            return Result.Success();
        }

        /// <summary>
        /// 添加员工 基础信息
        /// </summary>
        public Result AddOrEdit(Person model)
        {
            model.PinYinName = PingYinHelper.GetFirstSpell(model.UserName);
            //新增
            if (model.Id <= 0)
            {
                return base.Add(model);
            }
            else
            {
                return base.Update(model);
            }
        }

        /// <summary>
        /// 获取员工 信息
        /// </summary>
        public PersonDto GetPerson(int id)
        {
            return _repository.GetPerson(id);
        }
    }
}
