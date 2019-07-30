using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses.Wrappers
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PowerWrapper : BusinessWrapper
    {
        private readonly PowerModuleBusiness _moduleBusiness;
        private readonly PowerElementBusiness _elementBusiness;
        private readonly PowerRoleBusiness _roleBusiness;
        private readonly PowerRelevanceBusiness _relevanceBusiness;
        public PowerWrapper(PowerModuleBusiness moduleBusiness, PowerElementBusiness elementBusiness, PowerRoleBusiness roleBusiness, PowerRelevanceBusiness relevanceBusiness)
        {
            _moduleBusiness = moduleBusiness;
            _elementBusiness = elementBusiness;
            _roleBusiness = roleBusiness;
            _relevanceBusiness = relevanceBusiness;
        }

        /// <summary>
        /// 验证模块
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Result CheckModule(PowerModule model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("模块名称不能为空");
            }

            if (model.Id <= 0)
            {
                if (_moduleBusiness.Exist(p => p.Pid == model.Pid && p.Name == model.Name && p.Status == NomalStatus.Valid))
                {
                    return Result.Fail("模块名称已存在");
                }
                if (_moduleBusiness.Exist(p => p.Code == model.Code && p.Status == NomalStatus.Valid))
                {
                    return Result.Fail("模块Code已存在");
                }
            }
            else
            {
                if (_moduleBusiness.Exist(p => p.Pid == model.Pid && p.Name == model.Name && p.Status == NomalStatus.Valid && p.Id != model.Id))
                {
                    return Result.Fail("模块名称已存在");
                }
                if (_moduleBusiness.Exist(p => p.Code == model.Code && p.Status == NomalStatus.Valid && p.Id != model.Id))
                {
                    return Result.Fail("模块Code已存在");
                }
            }

            return Result.Success();
        }
        public Result AddOrEditModule(PowerModule model)
        {
            var result = CheckModule(model);
            if (!result.Succeed)
            {
                return result;
            }
            model.Status = NomalStatus.Valid;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    //新增
                    if (model.Id <= 0)
                    {
                        model.Level = 1;
                        model.LevelMap = "0";

                        _moduleBusiness.Add(model);
                    }

                    if (model.Pid <= 0)
                    {
                        model.LevelMap = ",0," + model.Id + ",";
                        model.Level = 1;
                    }
                    else
                    {
                        var modelParent = _moduleBusiness.GetSingle(p => p.Id == model.Pid && p.Status == NomalStatus.Valid);
                        if (modelParent == null)
                        {
                            return Result.Fail("未查询到父级模块");
                        }

                        model.LevelMap = modelParent.LevelMap + model.Id + ",";
                        model.Level = modelParent.Level + 1;

                        //删除父节点 模块元素中的 显示
                        _elementBusiness.Delete(p => p.ModuleId == modelParent.Id);
                    }

                    var showElemCount = _elementBusiness.Count(p => p.ModuleId == model.Id && p.DomId == "btnShow");
                    if (showElemCount <= 0 && model.Pid > 0)
                    {
                        var modelElement = new PowerElement()
                        {
                            Id = 0,
                            ModuleId = model.Id,
                            Name = "显示",
                            DomId = "btnShow",
                            Status = NomalStatus.Valid,
                            IsSystem = IsSystem.Yes,
                            Position = PowerElementPosition.Up,
                            DisplayOrder = -9999
                        };
                        _elementBusiness.Add(modelElement);
                    }

                    _moduleBusiness.Update(model);
                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("操作失败：" + ex.Message);
            }
        }
        public Result DeleteModule(List<int> ids)
        {
            if (ids.Count <= 0)
            {
                return Result.Fail("请勾选你要删除的数据");
            }
            return _moduleBusiness.UpdateBatch(ids);
        }


        /// <summary>
        /// 验证模块元素
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Result CheckElement(PowerElement model)
        {
            if (model.ModuleId <= 0)
            {
                return Result.Fail("模块不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("名称不能为空");
            }

            if (model.Id <= 0)
            {
                if (_elementBusiness.Exist(p => p.ModuleId == model.ModuleId && p.Name == model.Name && p.Status == NomalStatus.Valid))
                {
                    return Result.Fail("名称已存在");
                }
            }
            else
            {
                if (_elementBusiness.Exist(p => p.ModuleId == model.ModuleId && p.Name == model.Name && p.Status == NomalStatus.Valid && p.Id != model.Id))
                {
                    return Result.Fail("名称已存在");
                }
            }

            return Result.Success();
        }
        public Result AddOrEditElement(PowerElement model)
        {
            var result = CheckElement(model);
            if (!result.Succeed)
            {
                return result;
            }
            model.Status = NomalStatus.Valid;

            if (model.Id <= 0)
            {
                return _elementBusiness.Add(model);
            }
            else
            {
                return _elementBusiness.Update(model);
            }
        }
        public Result DeleteElement(List<int> ids)
        {
            if (ids.Count <= 0)
            {
                return Result.Fail("请勾选你要删除的数据");
            }
            return _elementBusiness.UpdateBatch(ids);
        }


        /// <summary>
        /// 验证角色
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Result CheckRole(PowerRole model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("名称不能为空");
            }
            if (model.Elements.Count() <= 0)
            {
                return Result.Fail("请勾选对应的权限");
            }

            if (model.Id <= 0)
            {
                if (_roleBusiness.Exist(p => p.Name == model.Name && p.Status == NomalStatus.Valid))
                {
                    return Result.Fail("名称已存在");
                }
            }
            else
            {
                if (_roleBusiness.Exist(p => p.Name == model.Name && p.Status == NomalStatus.Valid && p.Id != model.Id))
                {
                    return Result.Fail("名称已存在");
                }
            }

            return Result.Success();
        }
        public Result AddOrEditRole(PowerRole model)
        {
            var result = CheckRole(model);
            if (!result.Succeed)
            {
                return result;
            }
            model.Status = NomalStatus.Valid;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    //新增
                    if (model.Id <= 0)
                    {
                        _roleBusiness.Add(model);
                    }

                    //先删除角色对应的模块和模块元素
                    _relevanceBusiness.Delete(p => p.FirstId == model.Id);

                    //模块数据去重
                    List<int> lsDistinctModule = model.Elements.Select(t => t.ModuleId).Distinct().ToList();
                    if (lsDistinctModule.Count() == 0)
                    {
                        return Result.Fail("未获取到录入的数据");
                    }

                    //获取 模块数据
                    var dbModule = _moduleBusiness.Query(p => p.Status == NomalStatus.Valid);

                    //多对多关系集中映射表
                    var lsRelevance = new List<PowerRelevance>();

                    //保存对应的模块
                    var lsModule = new List<PowerModule>();
                    //循环并拿去 模块的所有父节点
                    foreach (var item in lsDistinctModule)
                    {
                        var nodeModule = GetFatherList(dbModule, item);
                        lsModule.AddRange(nodeModule);
                    }

                    //拿到所有模块 去重
                    List<int> distinctModule = lsModule.Select(t => t.Id).Distinct().ToList();
                    foreach (var item in distinctModule)
                    {
                        lsRelevance.Add(new PowerRelevance()
                        {
                            Id = 0,
                            FirstId = model.Id,
                            SecondId = item,
                            Identifiers = PowerIdentifiers.RoleByModule,
                            Remarks = PowerIdentifiers.RoleByModule.GetDescription()
                        });
                    }
                    if (lsDistinctModule.Count() == 0)
                    {
                        return Result.Fail("未获取到录入的数据");
                    }

                    //角色对应的模块元素
                    foreach (var item in model.Elements)
                    {
                        lsRelevance.Add(new PowerRelevance()
                        {
                            Id = 0,
                            FirstId = model.Id,
                            SecondId = item.Id,
                            Identifiers = PowerIdentifiers.RoleByElement,
                            Remarks = PowerIdentifiers.RoleByElement.GetDescription()
                        });
                    }

                    _roleBusiness.Update(model);
                    _relevanceBusiness.AddRange(lsRelevance);
                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("操作失败：" + ex.Message);
            }
        }
        public Result DeleteRole(List<int> ids)
        {
            if (ids.Count <= 0)
            {
                return Result.Fail("请勾选你要删除的数据");
            }
            return _roleBusiness.UpdateBatch(ids);
        }
        public Result EditRoleByEmployee(EmployeePowerRoleSubmitDto modelDto)
        {
            try
            {
                if (modelDto.EmployeeId <= 0)
                {
                    return Result.Fail("请先填写个人信息");
                }

                using (TransactionScope ts = new TransactionScope())
                {
                    //先删除用户对应角色
                    _relevanceBusiness.Delete(p => p.FirstId == modelDto.EmployeeId);

                    //多对多关系集中映射表
                    var lsRelevance = new List<PowerRelevance>();
                    foreach (var item in modelDto.RoleIds)
                    {
                        lsRelevance.Add(new PowerRelevance()
                        {
                            Id = 0,
                            FirstId = modelDto.EmployeeId,
                            SecondId = item,
                            Identifiers = PowerIdentifiers.UserByRole,
                            Remarks = PowerIdentifiers.UserByRole.GetDescription()
                        });
                    }

                    if (modelDto.RoleIds.Count() > 0)
                    {
                        _relevanceBusiness.AddRange(lsRelevance);
                    }

                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("操作失败：" + ex.Message);
            }
        }


        /// <summary>
        /// [列表] 迭代
        /// </summary>
        public List<PowerModuleDto> QueryNav(int roleId)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("Level", OrderBy.Asc),
                    new KeyValuePair<string, OrderBy>("DisplayOrder", OrderBy.Asc),
                };

            var oldList = _moduleBusiness.Query(p => p.Code != "050001" & p.Status == NomalStatus.Valid, p => p, order).MapToList<PowerModuleDto>();

            //获取角色对应的模块元素
            var lsCheckElement = new List<PowerRelevance>();
            if (roleId > 0)
            {
                lsCheckElement = _relevanceBusiness.Query(p => p.FirstId == roleId & p.Identifiers == PowerIdentifiers.RoleByElement).ToList();
            }
            //获取 模块元素
            var lsElement = _elementBusiness.Query(p => p.Status == NomalStatus.Valid, p => new PowerElement()
            {
                Id = p.Id,
                ModuleId = p.ModuleId,
                Name = p.Name,
                DomId = p.DomId,
                DisplayOrder = p.DisplayOrder,
                IsSystem = p.IsSystem,
                Class = p.Class,
                Position = p.Position,
                IconName = p.IconName,
                Checkeds = roleId == 0 ? 0 : lsCheckElement.Count(p2 => p2.SecondId == p.Id)
            }).ToList();

            var newList = new List<PowerModuleDto>();
            //调用迭代组合成List
            GetChilds(oldList, newList, 0, 0);

            var resultList = new List<PowerModuleDto>();
            foreach (var item in newList)
            {
                item.Elements = lsElement.Where(p => p.ModuleId == item.Id).OrderBy(p => p.DisplayOrder).ToList();
            }

            return newList;
        }
        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(List<PowerModuleDto> oldList, List<PowerModuleDto> newList, int pId, int level)
        {
            level++;
            var list = oldList.Where(t => t.Pid == pId).OrderBy(t => t.DisplayOrder);
            foreach (var item in list)
            {
                item.Level = level;
                newList.Add(item);
                this.GetChilds(oldList, newList, item.Id, level);
            }
        }

        /// <summary>
        /// 获取所有上级
        /// </summary>
        public static List<PowerModule> GetFatherList(IList<PowerModule> list, int id)
        {
            var query = list.Where(p => p.Id == id).ToList();
            return query.ToList().Concat(query.ToList().SelectMany(p => GetFatherList(list, p.Pid))).ToList();
        }

    }
}
