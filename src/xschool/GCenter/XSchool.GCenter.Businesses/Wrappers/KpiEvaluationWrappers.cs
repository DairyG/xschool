using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses.Wrappers
{
    internal class DynaimcTwoScoreAndStatuesWrapper
    {
        public DynaimcTwoScoreAndStatuesWrapper(PropertyInfo score, PropertyInfo status)
        {
            this.Score = score;
            this.Status = status;
        }

        public PropertyInfo Score { get; }

        public PropertyInfo Status { get; }
    }

    internal static class DynaimcHelper
    {
        static DynaimcHelper()
        {
            var lsScore = typeof(KpiManageTotal).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(p => p.Name.Contains("Score")).ToArray();
            var lsStatus = typeof(KpiManageTotal).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(p => p.Name.Contains("Status")).ToArray();
            if (lsScore.Length != lsStatus.Length)
            {
                throw new Exception("字段参数不一致");
            }

            for (int i = 0; i < lsScore.Length; i++)
            {
                var wrapper = new DynaimcTwoScoreAndStatuesWrapper(lsScore[i], lsStatus[i]);
                DynamicFileds.Add(wrapper);
            }
        }
        public static IList<DynaimcTwoScoreAndStatuesWrapper> DynamicFileds = new List<DynaimcTwoScoreAndStatuesWrapper>(12);
    }

    public class KpiEvaluationWrappers : BusinessWrapper
    {
        private readonly KpiTemplateRecordBusiness _tplRecordBusiness;
        private readonly KpiTemplateBusiness _tplBusiness;
        private readonly KpiTemplateDetailBusiness _tplDetailBusiness;
        private readonly KpiTemplateAuditRecordBusiness _tplAuditRecordBusiness;

        private readonly KpiManageTotalBusiness _magTotalBusiness;
        private readonly KpiManageRecordBusiness _magRecordBusiness;
        private readonly KpiManageDetailBusiness _magDetailBusiness;
        private readonly KpiManageAuditDetailBusiness _magAuditDetailBusiness;
        public KpiEvaluationWrappers(
            KpiTemplateRecordBusiness tplRecordBusiness,
            KpiTemplateBusiness tplBusiness,
            KpiTemplateDetailBusiness tplDetailBusiness,
            KpiTemplateAuditRecordBusiness tplAuditRecordBusiness,
            KpiManageTotalBusiness magTotalBusiness,
            KpiManageRecordBusiness magRecordBusiness,
            KpiManageDetailBusiness magDetailBusiness,
            KpiManageAuditDetailBusiness magAuditDetailBusiness
            )
        {
            _tplRecordBusiness = tplRecordBusiness;
            _tplBusiness = tplBusiness;
            _tplDetailBusiness = tplDetailBusiness;
            _tplAuditRecordBusiness = tplAuditRecordBusiness;

            _magTotalBusiness = magTotalBusiness;
            _magRecordBusiness = magRecordBusiness;
            _magDetailBusiness = magDetailBusiness;
            _magAuditDetailBusiness = magAuditDetailBusiness;
        }

        /// <summary>
        /// [验证] 考核模板
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        private Result CheckTemplat(KpiEvaluationTemplatSubmitDto modelDto)
        {
            if (modelDto.TemplateRecord.Count == 0)
            {
                return Result.Fail("请选择考核对象");
            }
            if (modelDto.TemplateAuditRecord.Count == 0)
            {
                return Result.Fail("请设置审核人");
            }
            if (modelDto.TemplateDetail.Count == 0)
            {
                return Result.Fail("请设置考核内容");
            }

            return Result.Success();
        }

        /// <summary>
        /// [添加/编辑] 考核模板
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        public Result AddOrEditTemplat(KpiEvaluationTemplatSubmitDto modelDto)
        {
            try
            {
                var dtNow = DateTime.Now;
                var currTime = 0;
                var result = CheckTemplat(modelDto);
                if (!result.Succeed)
                {
                    return result;
                }

                using (TransactionScope ts = new TransactionScope())
                {
                    //保存 考核模板记录
                    var dbTplRecord = new List<KpiTemplateRecord>();
                    //保存 考核模板
                    var dbTpl = new List<KpiTemplate>();
                    //保存 考核管理统计
                    var dbMagTotal = new List<KpiManageTotal>();

                    var whereTplRecord = new Condition<KpiTemplateRecord>();
                    whereTplRecord.And(p => p.KpiType == modelDto.KpiType);
                    whereTplRecord.And(p => p.KpiId == modelDto.KpiId);

                    if (modelDto.KpiType == KpiType.Dept) //部门
                    {
                        var dptIds = modelDto.TemplateRecord.Select(p => p.DptId);
                        whereTplRecord.And(p => dptIds.Contains(p.DptId));
                        dbTplRecord = _tplRecordBusiness.Query(whereTplRecord.Combine()).ToList();
                        dbTpl = _tplBusiness.Query(p => p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                        dbMagTotal = _magTotalBusiness.Query(p => p.Year == dtNow.Year && p.KpiId == modelDto.KpiId && p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                    }
                    else //人员
                    {
                        var employeeIds = modelDto.TemplateRecord.Select(p => p.EmployeeId);
                        whereTplRecord.And(p => employeeIds.Contains(p.EmployeeId));
                        dbTplRecord = _tplRecordBusiness.Query(whereTplRecord.Combine()).ToList();
                        dbTpl = _tplBusiness.Query(p => p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();

                        dbMagTotal = _magTotalBusiness.Query(p => p.Year == dtNow.Year && p.KpiId == modelDto.KpiId && p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();
                    }

                    var lsExistTplRecord = new List<KpiTemplateRecord>();
                    var lsNotExistTplRecord = new List<KpiTemplateRecord>();

                    var lsNotExistMagTotal = new List<KpiManageTotal>();

                    var distinct = dbTplRecord.ToDictionary(p => $"{p.CompanyId}_{p.DptId}_{p.EmployeeId}_{(int)p.KpiId}_{(int)p.KpiType}");
                    foreach (var item in modelDto.TemplateRecord)
                    {
                        var key = $"{item.CompanyId}_{item.DptId}_{item.EmployeeId}_{(int)item.KpiId}_{(int)item.KpiType}";
                        if (distinct.ContainsKey(key)) //已经存在
                        {
                            lsExistTplRecord.Add(distinct[key]);
                        }
                        else
                        {
                            lsNotExistTplRecord.Add(item);
                        }
                    }

                    if (lsNotExistTplRecord.Count > 0)
                    {
                        _tplRecordBusiness.AddRange(lsNotExistTplRecord);
                    }

                    //填充 不存在和存在的数据
                    var lsTplRecord = new List<KpiTemplateRecord>();
                    lsTplRecord.AddRange(lsNotExistTplRecord);
                    lsTplRecord.AddRange(lsExistTplRecord);

                    //考核模板明细
                    var lsTplDetail = new List<KpiTemplateDetail>();
                    //考核模板审核记录
                    var lsTplAuditRecord = new List<KpiTemplateAuditRecord>();
                    //考核模板
                    var lsExistTpl = new List<KpiTemplate>();
                    var lsNotExistTpl = new List<KpiTemplate>();

                    foreach (var record in lsTplRecord)
                    {
                        foreach (var itemDetail in modelDto.TemplateDetail)
                        {
                            lsTplDetail.Add(new KpiTemplateDetail
                            {
                                Id = 0,
                                KpiTemplateRecordId = record.Id,
                                CompanyId = record.CompanyId,
                                DptId = record.DptId,
                                EmployeeId = record.EmployeeId,
                                EvaluationId = itemDetail.EvaluationId,
                                EvaluationName = itemDetail.EvaluationName,
                                EvaluationType = itemDetail.EvaluationType,
                                Weight = itemDetail.Weight,
                                Explain = itemDetail.Explain
                            });
                        }

                        foreach (var itemAudit in modelDto.TemplateAuditRecord)
                        {
                            lsTplAuditRecord.Add(new KpiTemplateAuditRecord
                            {
                                Id = 0,
                                KpiTemplateRecordId = record.Id,
                                Steps = itemAudit.Steps,
                                CompanyId = itemAudit.CompanyId,
                                CompanyName = itemAudit.CompanyName,
                                DptId = itemAudit.DptId,
                                DptName = itemAudit.DptName,
                                JobId = itemAudit.JobId,
                                JobName = itemAudit.JobName
                            });
                        }

                        #region 考核模板

                        KpiTemplate modelTpl = null;
                        if (dbTpl.Count > 0)
                        {
                            if (modelDto.KpiType == KpiType.Dept) //部门
                            {
                                modelTpl = dbTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId);
                            }
                            else //人员
                            {
                                modelTpl = dbTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId && p.EmployeeId == record.EmployeeId);
                            }
                        }
                        if (modelTpl == null)
                        {
                            lsNotExistTpl.Add(new KpiTemplate()
                            {
                                Id = 0,
                                KpiType = record.KpiType,
                                CompanyId = record.CompanyId,
                                CompanyName = record.CompanyName,
                                DptId = record.DptId,
                                DptName = record.DptName,
                                EmployeeId = record.EmployeeId,
                                UserName = record.UserName,
                                Monthly = modelDto.KpiId == KpiPlan.Monthly ? record.Id : 0,
                                Quarter = modelDto.KpiId == KpiPlan.Quarter ? record.Id : 0,
                                HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? record.Id : 0,
                                Annual = modelDto.KpiId == KpiPlan.Annual ? record.Id : 0,
                            });
                        }
                        else
                        {
                            modelTpl.Monthly = modelDto.KpiId == KpiPlan.Monthly ? record.Id : modelTpl.Monthly;
                            modelTpl.Quarter = modelDto.KpiId == KpiPlan.Quarter ? record.Id : modelTpl.Quarter;
                            modelTpl.HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? record.Id : modelTpl.HalfYear;
                            modelTpl.Annual = modelDto.KpiId == KpiPlan.Annual ? record.Id : modelTpl.Annual;
                            lsExistTpl.Add(modelTpl);
                        }

                        #endregion

                        #region 考核管理统计

                        KpiManageTotal modelMagTotal = null;
                        if (dbMagTotal.Count > 0)
                        {
                            if (modelDto.KpiType == KpiType.Dept) //部门
                            {
                                modelMagTotal = dbMagTotal.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId);
                            }
                            else //人员
                            {
                                modelMagTotal = dbMagTotal.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId && p.EmployeeId == record.EmployeeId);
                            }
                        }
                        if (modelMagTotal == null)
                        {
                            var tempMagTotal = new KpiManageTotal()
                            {
                                Id = 0,
                                KpiType = record.KpiType,
                                KpiId = record.KpiId,
                                KpiName = record.KpiName,
                                CompanyId = record.CompanyId,
                                CompanyName = record.CompanyName,
                                DptId = record.DptId,
                                DptName = record.DptName,
                                EmployeeId = record.EmployeeId,
                                UserName = record.UserName,
                                Year = DateTime.Now.Year
                            };

                            switch (record.KpiId)
                            {
                                case KpiPlan.Monthly:
                                    currTime = dtNow.Month;
                                    break;
                                case KpiPlan.Quarter:
                                    currTime = TimeHelper.GetQurater(dtNow);
                                    break;
                                case KpiPlan.HalfYear:
                                    currTime = dtNow.Month < 7 ? 1 : 2;
                                    break;
                                case KpiPlan.Annual:
                                    currTime = 1;
                                    break;
                            }
                            for (int i = 0; i < 12; i++)
                            {
                                DynaimcHelper.DynamicFileds[i].Score.SetValue(tempMagTotal, (decimal)0);
                                if ((i + 1) < currTime)
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(tempMagTotal, KpiStatus.Invalid);
                                }
                                else if ((i + 1) == currTime)
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(tempMagTotal, KpiStatus.Init);
                                }
                                else
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(tempMagTotal, KpiStatus.NotStarted);
                                }
                            }

                            lsNotExistMagTotal.Add(tempMagTotal);
                        }

                        #endregion
                    }

                    if (lsExistTplRecord.Count > 0)
                    {
                        var templateRecordIds = lsExistTplRecord.Select(p => p.Id);
                        _tplDetailBusiness.Delete(p => templateRecordIds.Contains(p.KpiTemplateRecordId));
                        _tplAuditRecordBusiness.Delete(p => templateRecordIds.Contains(p.KpiTemplateRecordId));
                    }

                    _tplDetailBusiness.AddRange(lsTplDetail);
                    _tplAuditRecordBusiness.AddRange(lsTplAuditRecord);
                    if (lsNotExistMagTotal.Count > 0)
                    {
                        _magTotalBusiness.AddRange(lsNotExistMagTotal);
                    }
                    if (lsNotExistTpl.Count > 0)
                    {
                        _tplBusiness.AddRange(lsNotExistTpl);
                    }
                    if (lsExistTpl.Count > 0)
                    {
                        _tplBusiness.UpdateRange(lsExistTpl);
                    }

                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("添加失败：" + ex.Message);
            }
        }

        /// <summary>
        /// [生成] 考核记录
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        public Result GeneratedSingleByManage(KpiEvaluationManageQueryDto modelDto)
        {
            try
            {
                var dtNow = DateTime.Now;
                var currTime = 0;
                switch (modelDto.KpiId)
                {
                    case KpiPlan.Monthly:
                        currTime = dtNow.Month;
                        break;
                    case KpiPlan.Quarter:
                        currTime = TimeHelper.GetQurater(dtNow);
                        break;
                    case KpiPlan.HalfYear:
                        currTime = dtNow.Month < 7 ? 1 : 2;
                        break;
                    case KpiPlan.Annual:
                        currTime = 1;
                        break;
                }
                var kpiDate = string.IsNullOrWhiteSpace(modelDto.KpiDate) ? currTime.ToString() : modelDto.KpiDate;

                using (TransactionScope ts = new TransactionScope())
                {
                    var dbMagRecordCount = 0;
                    if (modelDto.KpiType == KpiType.Dept) //部门
                    {
                        dbMagRecordCount = _magRecordBusiness.Count(p => p.KpiType == modelDto.KpiType && p.KpiId == modelDto.KpiId && p.CompanyId == modelDto.CompanyId && p.DptId == modelDto.DptId && p.Year == modelDto.Year && p.KpiDate == kpiDate);
                    }
                    else //人员
                    {
                        dbMagRecordCount = _magRecordBusiness.Count(p => p.KpiType == modelDto.KpiType && p.KpiId == modelDto.KpiId && p.CompanyId == modelDto.CompanyId && p.DptId == modelDto.DptId && p.EmployeeId == modelDto.EmployeeId && p.Year == modelDto.Year && p.KpiDate == kpiDate);
                    }
                    if (dbMagRecordCount > 0)
                    {
                        return Result.Fail("数据已生成", "201");
                    }

                    //获取 考核模板记录
                    KpiTemplateRecord dbTplRecord = null;
                    //获取 考核管理统计
                    KpiManageTotal dbMagTotal = null;
                    if (modelDto.KpiType == KpiType.Dept) //部门
                    {
                        dbTplRecord = _tplRecordBusiness.GetSingle(p => p.KpiType == modelDto.KpiType && p.KpiId == modelDto.KpiId && p.CompanyId == modelDto.CompanyId && p.DptId == modelDto.DptId);
                        dbMagTotal = _magTotalBusiness.GetSingle(p => p.KpiType == modelDto.KpiType && p.KpiId == modelDto.KpiId && p.CompanyId == modelDto.CompanyId && p.DptId == modelDto.DptId && p.Year == modelDto.Year);
                    }
                    else //人员
                    {
                        dbTplRecord = _tplRecordBusiness.GetSingle(p => p.KpiType == modelDto.KpiType && p.KpiId == modelDto.KpiId && p.CompanyId == modelDto.CompanyId && p.DptId == modelDto.DptId && p.EmployeeId == modelDto.EmployeeId);
                        dbMagTotal = _magTotalBusiness.GetSingle(p => p.KpiType == modelDto.KpiType && p.KpiId == modelDto.KpiId && p.CompanyId == modelDto.CompanyId && p.DptId == modelDto.DptId && p.EmployeeId == modelDto.EmployeeId && p.Year == modelDto.Year);
                    }

                    if (dbTplRecord == null || dbMagTotal == null)
                    {
                        return Result.Fail("未获取到对应的记录");
                    }

                    //获取 考核模板明细
                    var lsTplDetail = _tplDetailBusiness.Query(p => p.KpiTemplateRecordId == dbTplRecord.Id).OrderBy(p => p.Id);

                    //考核管理统计
                    for (int i = 0; i < 12; i++)
                    {
                        if ((i + 1) == currTime)
                        {
                            DynaimcHelper.DynamicFileds[i].Status.SetValue(dbMagTotal, KpiStatus.Zero);
                        }
                    }

                    //考核管理记录
                    var modelMagRecord = new KpiManageRecord()
                    {
                        Id = 0,
                        KpiTemplateRecordId = dbTplRecord.Id,
                        KpiType = dbTplRecord.KpiType,
                        KpiId = dbTplRecord.KpiId,
                        KpiName = dbTplRecord.KpiName,
                        CompanyId = dbTplRecord.CompanyId,
                        CompanyName = dbTplRecord.CompanyName,
                        DptId = dbTplRecord.DptId,
                        DptName = dbTplRecord.DptName,
                        EmployeeId = dbTplRecord.EmployeeId,
                        UserName = dbTplRecord.UserName,
                        Year = modelDto.Year,
                        KpiDate = kpiDate,
                        Steps = KpiSteps.Zero,
                        StepsCompanyId = dbTplRecord.CompanyId,
                        StepsCompanyName = dbTplRecord.CompanyName,
                        StepsDptId = dbTplRecord.DptId,
                        StepsDptName = dbTplRecord.DptName,
                        StepsEmployeeId = dbTplRecord.EmployeeId,
                        StepsUserName = dbTplRecord.UserName,
                        AddDate = dtNow,
                        Status = KpiStatus.Zero
                    };
                    _magRecordBusiness.Add(modelMagRecord);

                    //考核管理明细
                    var lsMagDetail = new List<KpiManageDetail>();
                    foreach (var tplDetail in lsTplDetail)
                    {
                        lsMagDetail.Add(new KpiManageDetail()
                        {
                            Id = 0,
                            KpiManageRecordId = modelMagRecord.Id,                            
                            CompanyId = modelMagRecord.CompanyId,
                            DptId = modelMagRecord.DptId,
                            EmployeeId = modelMagRecord.EmployeeId,
                            EvaluationId = tplDetail.EvaluationId,
                            EvaluationName = tplDetail.EvaluationName,
                            EvaluationType = tplDetail.EvaluationType,
                            Weight = tplDetail.Weight,
                            Explain = tplDetail.Explain,
                            Year = modelMagRecord.Year,
                            KpiDate = modelMagRecord.KpiDate,
                            SelfScore = 0,
                            OneScore = 0,
                            TwoScore = 0,
                        });
                    }
                    _magDetailBusiness.AddRange(lsMagDetail);
                    _magTotalBusiness.Update(dbMagTotal);

                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("添加失败：" + ex.Message);
            }
        }

    }
}
