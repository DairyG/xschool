using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.Helpers;
using XSchool.WorkFlow.Businesses;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class SubjectController : ApiBaseController
    {
        private readonly SubjectBusiness subjectBusiness;
        public SubjectController(SubjectBusiness _subjectBusiness)
        {
            this.subjectBusiness = _subjectBusiness;
        }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]SubjectDto modelDto)
        {
            
            Subject dataModel = new Subject();
            List<SubjectRule> subjectRuleRangList = Mapper.Map<List<SubjectRule>>(modelDto.SubjectRuleRangeList);//流程可见范围

            List<SubjectStep> subjectStepList = new List<SubjectStep>();//流程节点集合


            foreach (SubjectStepDto itemParents in modelDto.SubjectStepFlowList)
            {
                List<SubjectRule> empRulePassList = Mapper.Map<List<SubjectRule>>(itemParents.SubjectRulePassList);//节点审核人信息

                itemParents.SubjectRulePassList = null;
                var subjectStep = Mapper.Map<SubjectStep>(itemParents);
                subjectStep.SubjectRulePassList = empRulePassList;
                subjectStepList.Add(subjectStep);
            }

            modelDto.SubjectRuleRangeList = null;
            modelDto.SubjectStepFlowList = null;
            var model = Mapper.Map<Subject>(modelDto);
            model.SubjectRuleRangeList = subjectRuleRangList;
            model.SubjectStepFlowList = subjectStepList;
            model.Status = EDStatus.Enable;
            model.CreateTime = DateTime.Now;
            model.CompanyId = Emplolyee.CompanyId;
            model.CreateUserId = this.UToken.Id;
            //找寻最后一个审核节点并赋值为IsEnd
           var lastObj= model.SubjectStepFlowList.Where(s => s.PassType !=PassType.Copy).OrderBy(s => s.PassNo).Last();
            model.SubjectStepFlowList.Where(s => s.PassNo == lastObj.PassNo).ToList().ForEach(s => s.IsEnd = true);

             //最后节点若是复盘节点
             model.SubjectStepFlowList.Where(s => s.PassType == PassType.Summary&&s.IsEnd).ToList().ForEach(s=>s.IsCountersign=true);
            var dataResult=subjectBusiness.Add(model);
            return dataResult;
        }

        /// <summary>
        /// 编辑流程
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromForm]SubjectDto modelDto)
        {
            Subject dataModel = new Subject();
            List<SubjectRule> subjectRuleRangList = Mapper.Map<List<SubjectRule>>(modelDto.SubjectRuleRangeList);//流程可见范围
            List<SubjectStep> subjectStepList = new List<SubjectStep>();//流程节点集合


            foreach (SubjectStepDto itemParents in modelDto.SubjectStepFlowList)
            {
                List<SubjectRule> empRulePassList = Mapper.Map<List<SubjectRule>>(itemParents.SubjectRulePassList);//节点审核人信息

                itemParents.SubjectRulePassList = null;
                var subjectStep = Mapper.Map<SubjectStep>(itemParents);
                subjectStep.SubjectRulePassList = empRulePassList;
                subjectStep.SubjectId = modelDto.Id;
                subjectStepList.Add(subjectStep);
            }
           
            modelDto.SubjectRuleRangeList = null;
            modelDto.SubjectStepFlowList = null;
            var model = Mapper.Map<Subject>(modelDto);
            subjectRuleRangList.ForEach(s => s.SubjectId = modelDto.Id);
            model.SubjectRuleRangeList = subjectRuleRangList;
            model.SubjectStepFlowList = subjectStepList;
            return subjectBusiness.Edit(model);
        }


        /// <summary>
        /// 获取流程对象
        /// </summary>
        /// <param name="Id">流程id</param>
        /// <returns></returns>
        [HttpGet]
        public SubjectDto GetSubjectById(int Id)
        {
            var model = subjectBusiness.GetSubjectById(Id);
            
            return model;
        }

        /// <summary>
        /// 启用或禁用流程
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="status">1禁用，2启用</param>
        /// <returns></returns>
        [HttpPost]
        public Result EnableSubject([FromForm]int Id, [FromForm]int status)
        {
            return subjectBusiness.EnableSubject(Id, status);
        }


        /// <summary>
        /// 获取所有流程分组及流程内容(流程模板)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetSubject()
        {
            return subjectBusiness.GetSubject();
        }

        /// <summary>
        /// 获取所有启用的流程分组及流程内容(发起审批)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetEnableSubject()
        {
            return subjectBusiness.GetEnableSubject();
        }
        /// <summary>
        /// 修改流程可见范围
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result UpdateSubjectRange([FromForm]SubjectRangeDto model)
        {
            return subjectBusiness.UpdateSubjectRange(model);
        }

    }
}