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
    public class SubjectController : Controller
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
        public Result Add([FromBody]SubjectDto modelDto)
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
            model.CompanyId = 0;//当前登陆人公司id

            return subjectBusiness.Add(model);
        }

        /// <summary>
        /// 编辑流程
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromBody]SubjectDto modelDto)
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
    }
}