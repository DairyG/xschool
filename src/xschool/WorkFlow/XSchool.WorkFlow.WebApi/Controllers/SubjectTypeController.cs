using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.Helpers;
using XSchool.WorkFlow.Businesses;
using XSchool.WorkFlow.Model;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class SubjectTypeController : ApiBaseController
    {
        private readonly SubjectTypeBusiness subjectTypeBusiness;
        public SubjectTypeController(SubjectTypeBusiness _subjectTypeBusiness)
        {
            this.subjectTypeBusiness = _subjectTypeBusiness;
        }

        /// <summary>
        /// 添加或编辑流程组别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddOrEdit([FromForm]SubjectType model)
        {
            model.Status = EDStatus.Enable;
            model.IsDelete = false;
            return subjectTypeBusiness.AddOrEdit(model);
        }
        /// <summary>
        /// 获取启用的所有流程组别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<SubjectType> GetSubjectTypeList()
        {
            return subjectTypeBusiness.GetSubjectTypeList();
        }

        /// <summary>
        /// 获取所有流程类别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<KeyValuePair<int, string>> GetFlowTypeList()
        {
            var data = EnumHelper.GetEnumValueNameCollection(typeof(FlowType)).ToList();

            return data;
        }


        /// <summary>
        /// 删除流程组别
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public Result DeleteSubjectType([FromForm]int Id)
        {
            return subjectTypeBusiness.DeleteSubjectType(Id);
        }


        /// <summary>
        /// 删除流程组别 test1
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public Result testDel([FromForm]int Id)
        {
            return subjectTypeBusiness.DeleteSubjectType1(Id);

        }
    }
}