using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class SummaryController : ApiBaseController
    {
        private readonly SummaryBusiness _summaryBusiness;
        public class SummaryModel
        {
            public int Id { get; set; }
            /// <summary>
            /// 公司ID
            /// </summary>
            public int CompanyId { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public int DptId { get; set; }
            /// <summary>
            /// 员工ID
            /// </summary>
            public int EmployeeId { get; set; }
            /// <summary>
            /// 员工姓名
            /// </summary>
            public int EmployeeName { get; set; }
            /// <summary>
            /// 日期
            /// </summary>
            public string SummaryDate { get; set; }
            /// <summary>
            /// 完成的工作
            /// </summary>
            public string Finish { get; set; }
            /// <summary>
            /// 工作总结
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 计划/未完成的工作
            /// </summary>
            public string Plan { get; set; }
            /// <summary>
            /// 需协调和帮助
            /// </summary>
            public string Help { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// 上传时间
            /// </summary>
            public DateTime AddTime { get; set; }

            public string DateSummary { get; set; }
            public string ReadState { get; set; }
        }
        public SummaryController(SummaryBusiness summaryBusiness)
        {
            _summaryBusiness = summaryBusiness;
        }
        /// <summary>
        /// 根据类型查询所有总结
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("{type}")]
        public IList<SummaryModel> Get(int type)
        {
            IList<Summary> list = _summaryBusiness.Get(type);
            IList<SummaryModel> modelList = new List<SummaryModel>();
            foreach (var item in list)
            {
                SummaryModel model = new SummaryModel();
                model.Id = item.Id;
                model.CompanyId = item.CompanyId;
                model.DptId = item.DptId;
                model.EmployeeId = item.EmployeeId;
                model.SummaryDate = item.SummaryDate;
                model.Finish = item.Finish;
                model.Content = item.Content;
                model.Plan = item.Plan;
                model.Help = item.Help;
                model.Description = item.Description;
                model.AddTime = item.AddTime;
                model.DateSummary = item.SummaryDate + GetDescription(item.Index);
                model.ReadState = GetDescription(item.IsRead);
                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>
        /// 根据Id查询总结
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Summary GetSingle(int id)
        {
            return _summaryBusiness.GetInfo(id);
        }
        /// <summary>
        /// [添加] 总结
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]Summary model)
        {
            return _summaryBusiness.Add(model);
        }
        [HttpGet("{id}")]
        public int UpdateRead(int id)
        {
            return _summaryBusiness.UpdateRead(id);
        }

        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();   //获取类型
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性

                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述
                }
            }
            return en.ToString();
        }
        /// <summary>
        /// 删除报告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Delete([FromForm]int id)
        {
            return _summaryBusiness.Delete(id);
        }
    }
}