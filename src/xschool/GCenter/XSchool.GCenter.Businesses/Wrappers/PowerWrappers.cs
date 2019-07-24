using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.Businesses.Wrappers
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PowerWrappers : BusinessWrapper
    {
        private readonly PowerModuleBusiness _moduleBusiness;
        private readonly PowerElementBusiness _elementBusiness;
        public PowerWrappers(PowerModuleBusiness moduleBusiness, PowerElementBusiness elementBusiness)
        {
            _moduleBusiness = moduleBusiness;
            _elementBusiness = elementBusiness;
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
            }
            else
            {
                if (_moduleBusiness.Exist(p => p.Pid == model.Pid && p.Name == model.Name && p.Status == NomalStatus.Valid && p.Id != model.Id))
                {
                    return Result.Fail("模块名称已存在");
                }
            }

            return Result.Success();
        }

        public Result AddOrEdit(PowerModule model)
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

    }
}
