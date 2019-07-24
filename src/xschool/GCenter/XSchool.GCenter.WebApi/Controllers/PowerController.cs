using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.Helpers;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class PowerController : ApiBaseController
    {
        private readonly PowerModuleBusiness _moduleBusiness;
        private readonly PowerElementBusiness _elementBusiness;
        private readonly PowerWrappers _wrappers;
        public PowerController(PowerModuleBusiness moduleBusiness, PowerElementBusiness elementBusiness, PowerWrappers wrappers)
        {
            _moduleBusiness = moduleBusiness;
            _elementBusiness = elementBusiness;
            _wrappers = wrappers;
        }

        /// <summary>
        /// [列表] 模块
        /// </summary>
        /// <param name="mode">显示方式</param>
        /// <returns></returns>
        [HttpPost]
        public object QueryModulesByTree([FromForm]ModulesDisplayMode mode)
        {
            var lsMoudle = _moduleBusiness.Query(p => p.Status == NomalStatus.Valid, p => new
            {
                p.Id,
                p.Name,
                p.Pid,
                p.DisplayOrder
            });
            switch (mode)
            {
                case ModulesDisplayMode.AddAll:
                    lsMoudle.Add(new { Id = 0, Name = "全部", Pid = 0, DisplayOrder = -99999 });
                    break;
                case ModulesDisplayMode.AddRoot:
                    lsMoudle.Add(new { Id = 0, Name = "根节点", Pid = 0, DisplayOrder = -99999 });
                    break;
            }

            return lsMoudle.OrderBy(p => p.DisplayOrder);
        }

        /// <summary>
        /// [分页] 模块
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="pid">模块Id</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<PowerModule> QueryModule([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]int pid)
        {
            var condition = new Condition<PowerModule>();
            condition.And(p => p.Status == NomalStatus.Valid);
            condition.And(p => p.LevelMap.Contains("," + pid + ","));
            return _moduleBusiness.Page(page, limit, condition.Combine());
        }

        /// <summary>
        /// [添加/编辑] 模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EditModule([FromForm]PowerModule model)
        {
            return _wrappers.AddOrEdit(model);
        }


        /// <summary>
        /// [分页] 元素
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        /// <returns></returns>
        public IPageCollection<PowerElement> QueryElement([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]int moduleId)
        {
            var condition = new Condition<PowerElement>();
            condition.And(p => p.Status == NomalStatus.Valid);
            condition.And(p => p.ModuleId == moduleId);
            return _elementBusiness.Page(page, limit, condition.Combine());
        }

        /// <summary>
        /// 模块显示方式
        /// </summary>
        public enum ModulesDisplayMode
        {
            /// <summary>
            /// 原始
            /// </summary>
            [Description("原始")]
            Original = 1,

            /// <summary>
            /// 添加全部
            /// </summary>
            [Description("添加全部")]
            AddAll = 2,

            /// <summary>
            /// 添加根节点
            /// </summary>
            [Description("添加根节点")]
            AddRoot = 3,
        }

    }
}