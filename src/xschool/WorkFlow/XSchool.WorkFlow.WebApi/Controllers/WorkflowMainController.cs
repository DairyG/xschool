using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.WorkFlow.Businesses;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;
using XSchool.WorkFlow.WebApi.Helper;
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
        [HttpGet("{subjectId}")]
        public Result GetWorkFlowForm(int subjectId)
        {
            return workflowMainBusiness.GetWorkFlowForm(subjectId);
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
                CreateUserName = this.Emplolyee.EmployeeName,
                PassStatus = PassStatus.InApproval,
                FormAttribute = model.FormAttribute,
                FormContent = model.FormContent,
                CompanyId = this.Emplolyee.CompanyId,
                DepId = this.Emplolyee.DptId
            };
            return workflowMainBusiness.CreateWork(entity);
        }
        /// <summary>
        /// 待我审核
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public object WatiApprovalList([FromForm]WorkFlowDataViewDto viewModel,[FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {

            var model = Mapper.Map<WorkFlowDataPageDto>(viewModel);
            model.CreateUserId = UToken.Id;
            int totalCount = 0;
           var dataList= workflowMainBusiness.WaitApprove(model, page, limit, ref totalCount);
            return new { totalCount = totalCount, items = dataList };
        }
        /// <summary>
        ///  我发起的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetMyWorkFlowDataList([FromForm]WorkFlowMainFormDto model)
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
                CompanyId = this.Emplolyee.CompanyId
            };
            return workflowMainBusiness.CreateWork(entity);
        }


    }
}