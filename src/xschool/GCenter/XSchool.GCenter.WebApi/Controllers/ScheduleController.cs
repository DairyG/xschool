using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ScheduleController : ApiBaseController
    {
        private readonly ScheduleBusiness _scheduleBusiness;
        public ScheduleController(ScheduleBusiness scheduleBusiness)
        {
            _scheduleBusiness = scheduleBusiness;
        }
        public class ScheduleModel
        {
            public Schedule Sche { get; set; }
            public string Plan { get; set; }
            public string RemindTimeName { get; set; }
            public string RemindWayName { get; set; }
            public string EmergencyName { get; set; }
        }
        /// <summary>
        /// [添加] 日程
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]Schedule model)
        {
            return _scheduleBusiness.Add(model);
        }
        /// <summary>
        /// 获取全部日程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<Schedule> Get()
        {
            return _scheduleBusiness.Get();
        }

        /// <summary>
        /// 根据ID获取任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ScheduleModel GetSingle(int id)
        {
            ScheduleModel newmodel = new ScheduleModel();
            Schedule model = _scheduleBusiness.GetSingle(id);
            newmodel.Sche = model;
            newmodel.Plan = GetDescription(model.KpiPlan);
            newmodel.RemindTimeName = GetDescription(model.RemindTime);
            newmodel.RemindWayName = GetDescription(model.RemindWay);
            newmodel.EmergencyName = GetDescription(model.Emergency);
            return newmodel;
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
    }
}