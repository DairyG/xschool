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
        /// <param name="viewModel"></param>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public object WatiApprovalList([FromForm]WorkFlowDataViewDto viewModel,[FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {

            var model = Mapper.Map<WorkFlowDataPageDto>(viewModel);
            model.CreateUserId = UToken.Id;
            int totalCount = 0;
            var dataList= workflowMainBusiness.WatiApprovalList(model, page, limit, ref totalCount);
                return new { totalCount = totalCount, items = dataList };
        }
        /// <summary>
        ///  我已审批
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object ApprovalRecords([FromForm]WorkFlowDataViewDto viewModel, [FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {

            var model = Mapper.Map<WorkFlowDataPageDto>(viewModel);
            model.CreateUserId = UToken.Id;
            int totalCount = 0;
            var dataList = workflowMainBusiness.ApprovalRecords(model, page, limit, ref totalCount);
            return new { totalCount = totalCount, items = dataList };
        }
        /// <summary>
        ///  我发起的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object MyCreateApproval([FromForm]WorkFlowDataViewDto viewModel, [FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {

            var model = Mapper.Map<WorkFlowDataPageDto>(viewModel);
            model.CreateUserId = UToken.Id;
            int totalCount = 0;
            var dataList = workflowMainBusiness.MyCreateApproval(model, page, limit, ref totalCount);
            return new { totalCount = totalCount, items = dataList };
        }

        /// <summary>
        ///  抄送我的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object SendCopyMe([FromForm]WorkFlowDataViewDto viewModel, [FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {

            var model = Mapper.Map<WorkFlowDataPageDto>(viewModel);
            model.CreateUserId = UToken.Id;
            int totalCount = 0;
            var dataList = workflowMainBusiness.SendCopyMe(model, page, limit, ref totalCount);
            return new { totalCount = totalCount, items = dataList };
        }

        /// <summary>
        ///  撤销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Revoke([FromForm]int Id)
        {
            string msg = string.Empty;
            var status = workflowMainBusiness.Revoke(Id,ref msg);
            return new Result {  Succeed=status, Message= msg };
        }
        /// <summary>
        ///  审核人员变更
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result ApprovalPerson([FromForm]ApprovalPersonChageDto model)
        {
            string optName = this.Emplolyee.EmployeeName;
           var data  = workflowMainBusiness.ApprovalPerson(model, optName);
            return data;
        }

        /// <summary>
        /// 获取发起人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public WorkflowMainTestDto GetCreateLCInfo(int Id)
        {
           var obj= workflowMainBusiness.GetSingle(Id);
            var model = Mapper.Map<WorkflowMainTestDto>(obj);
            var objqwe = XSchool.WorkFlow.WebApi.Helper.RemoteRequestHelper.GetEmployeeByUserId(obj.CreateUserId).Result;
            model.DepName = objqwe.DptName;
            model.JobName = objqwe.JobName;
            model.CompanyName = objqwe.CompanyName;
            return model;
        }
    }
}