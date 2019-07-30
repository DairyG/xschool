using System;
using System.Collections.Generic;
using System.Linq;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses.Wrappers
{
    /// <summary>
    /// 员工 授权策略
    /// </summary>
    public class AuthStrategyWrapper : BusinessWrapper
    {
        private readonly PowerModuleBusiness _moduleBusiness;
        private readonly PowerElementBusiness _elementBusiness;
        private readonly PowerRoleBusiness _roleBusiness;
        private readonly PowerRelevanceBusiness _relevanceBusiness;
        public AuthStrategyWrapper(PowerModuleBusiness moduleBusiness, PowerElementBusiness elementBusiness, PowerRoleBusiness roleBusiness, PowerRelevanceBusiness relevanceBusiness)
        {
            _moduleBusiness = moduleBusiness;
            _elementBusiness = elementBusiness;
            _roleBusiness = roleBusiness;
            _relevanceBusiness = relevanceBusiness;
        }

        public AuthStrategyDto NormalAuthStrategy(int employeeId)
        {
            var result = new AuthStrategyDto();
            var userRoleIds = _relevanceBusiness.Query(p => p.FirstId == employeeId && p.Identifiers == PowerIdentifiers.UserByRole).Select(p => p.SecondId);
            if (userRoleIds.Count() == 0)
            {
                return result;
            }
            var roleIds = _roleBusiness.Query(p => userRoleIds.Contains(p.Id) && p.Status == NomalStatus.Valid).Select(p => p.Id);
            if (userRoleIds.Count() == 0)
            {
                return result;
            }

            //先拉取角色对应的模块和元素
            var relevanceIds = _relevanceBusiness.Query(p => roleIds.Contains(p.FirstId));
            var moduleIds = relevanceIds.Where(p => p.Identifiers == PowerIdentifiers.RoleByModule).Select(p => p.SecondId);
            if (moduleIds.Count() == 0)
            {
                return result;
            }

            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("Level", OrderBy.Asc),
                    new KeyValuePair<string, OrderBy>("DisplayOrder", OrderBy.Asc)
                };
            var modules = _moduleBusiness.Query(p => moduleIds.Contains(p.Id) && p.Status == NomalStatus.Valid, p => p, order);

            result.Modules = modules.Select(p => new PowerModuleDto()
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                Level = p.Level,
                Pid = p.Pid,
                Url = p.Url,
                IconName = p.IconName
            }).GenerateTree(p => p.Id, p => p.Pid).ToList();

            //模块元素
            var elementIds = relevanceIds.Where(p => p.Identifiers == PowerIdentifiers.RoleByElement).Select(p => p.SecondId);
            var elements = _elementBusiness.Query(p => elementIds.Contains(p.Id) && p.IsSystem == 0 && p.Status == NomalStatus.Valid);
            result.Elements = modules.Select(p => new PowerModuleElementDto()
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Elements = elements.Where(p1 => p1.ModuleId == p.Id).OrderBy(p1 => p1.DisplayOrder).ToList()
            }).ToList();

            return result;
        }
    }
}
