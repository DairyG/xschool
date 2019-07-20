using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logistics.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.WorkFlow.Businesses;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.WebApi.Controllers
{
    /// <summary>
    /// 流程节点
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class WorkflowMainController : ApiBaseController
    {
        private readonly WorkflowMainBusiness workflowMainBusiness;
        public WorkflowMainController(WorkflowMainBusiness _workflowMainBusiness)
        {
            this.workflowMainBusiness = _workflowMainBusiness;
        }

        /// <summary>
        /// 根据流程Id获取内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetWorkFlowForm(int SubjectId)
        {
            return workflowMainBusiness.GetWorkFlowForm(SubjectId);
        }


        /// <summary>
        ///  发起审批
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result CreateWork([FromForm]WorkFlowMainFormDto model)
        {
            WorkflowMain entity = new WorkflowMain
            {
                SubjectId = model.SubjectId,
                Createtime = DateTime.Now,
                CreateUserId = UToken.Id,
                CreateUserName = UToken.UserName,
                PassStatus = PassStatus.InApproval,
                FormAttribute = model.FormAttribute,
                FormContent = model.FormContent,
                 CompanyId=this.Emplolyee.CompanyId
            };


            return workflowMainBusiness.CreateWork(entity);
        }

    }
}