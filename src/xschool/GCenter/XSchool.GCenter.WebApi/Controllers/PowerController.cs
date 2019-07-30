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
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [RequestFormLimits(ValueCountLimit = 5000)]
    public class PowerController : ApiBaseController
    {
        private readonly PowerModuleBusiness _moduleBusiness;
        private readonly PowerElementBusiness _elementBusiness;
        private readonly PowerRoleBusiness _roleBusiness;
        private readonly PowerRelevanceBusiness _relevanceBusiness;
        private readonly PowerWrapper _wrappers;
        public PowerController(PowerModuleBusiness moduleBusiness, PowerElementBusiness elementBusiness, PowerRoleBusiness roleBusiness, PowerRelevanceBusiness relevanceBusiness, PowerWrapper wrappers)
        {
            _moduleBusiness = moduleBusiness;
            _elementBusiness = elementBusiness;
            _roleBusiness = roleBusiness;
            _relevanceBusiness = relevanceBusiness;
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
            return _moduleBusiness.Page(page, limit, pid);
        }
        /// <summary>
        /// [添加/编辑] 模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EditModule([FromForm]PowerModule model)
        {
            return _wrappers.AddOrEditModule(model);
        }
        /// <summary>
        /// [删除] 模块
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns></returns>
        [HttpPost]
        public Result DeleteModule([FromForm]List<int> ids)
        {
            return _wrappers.DeleteModule(ids);
        }


        /// <summary>
        /// [分页] 元素
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<PowerElement> QueryElement([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]int moduleId)
        {
            var condition = new Condition<PowerElement>();
            condition.And(p => p.Status == NomalStatus.Valid);
            condition.And(p => p.ModuleId == moduleId);

            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("DisplayOrder", OrderBy.Asc)
                };
            return _elementBusiness.Page(page, limit, condition.Combine(), p => p, order);
        }
        /// <summary>
        /// [添加/编辑] 模板元素
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EditElement([FromForm]PowerElement model)
        {
            return _wrappers.AddOrEditElement(model);
        }
        /// <summary>
        /// [删除] 模块元素
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns></returns>
        [HttpPost]
        public Result DeleteElement([FromForm]List<int> ids)
        {
            return _wrappers.DeleteElement(ids);
        }


        /// <summary>
        /// [列表] 角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object QueryRole()
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("DisplayOrder", OrderBy.Asc)
                };
            return _roleBusiness.Query(p => p.Status == NomalStatus.Valid, p => new { p.Id, p.Name }, order);
        }
        /// <summary>
        /// [详情] 角色
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public PowerRole GetRole(int id)
        {
            return _roleBusiness.GetSingle(p => p.Id == id);
        }
        /// <summary>
        /// [分页] 角色
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="name">角色名称</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<PowerRole> QueryRole([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]string name)
        {
            var condition = new Condition<PowerRole>();
            condition.And(p => p.Status == NomalStatus.Valid);
            if (!string.IsNullOrWhiteSpace(name))
            {
                condition.And(p => p.Name.Contains(name));
            }

            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("DisplayOrder", OrderBy.Asc)
                };
            return _roleBusiness.Page(page, limit, condition.Combine(), p => p, order);
        }
        /// <summary>
        /// [添加/编辑] 角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EditRole([FromForm]PowerRole model)
        {
            return _wrappers.AddOrEditRole(model);
        }
        /// <summary>
        /// [删除] 角色
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns></returns>
        [HttpPost]
        public Result DeleteRole([FromForm]List<int> ids)
        {
            return _wrappers.DeleteRole(ids);
        }
        /// <summary>
        /// [设置] 员工的角色
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EditRoleByEmployee([FromForm]EmployeePowerRoleSubmitDto modelDto)
        {
            return _wrappers.EditRoleByEmployee(modelDto);
        }

        /// <summary>
        /// [列表] 模块，迭代
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public List<PowerModuleDto> QueryNav(int roleId)
        {
            return _wrappers.QueryNav(roleId);
        }
        /// <summary>
        /// [列表] 根据角色Id查询模块元素
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public object QueryElementByRole(int roleId)
        {
            return _relevanceBusiness.Query(p => p.FirstId == roleId & p.Identifiers == PowerIdentifiers.RoleByElement, p => new
            {
                p.FirstId,
                p.SecondId
            }).ToList();
        }
        /// <summary>
        /// [列表] 根据用户Id查询角色
        /// </summary>
        /// <param name="employeeId">用户Id</param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        public object QueryRoleByEmployee(int employeeId)
        {
            return _relevanceBusiness.Query(p => p.FirstId == employeeId & p.Identifiers == PowerIdentifiers.UserByRole, p => new
            {
                p.FirstId,
                p.SecondId
            }).ToList();
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