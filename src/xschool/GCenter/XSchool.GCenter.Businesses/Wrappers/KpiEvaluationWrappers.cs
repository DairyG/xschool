using Newtonsoft.Json;
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

    /// <summary>
    /// 绩效考核
    /// </summary>
    public class KpiEvaluationWrappers : BusinessWrapper
    {
        private readonly KpiTemplateBusiness _tplBusiness;
        private readonly KpiTemplateRecordBusiness _tplRecordBusiness;

        private readonly KpiManageTotalBusiness _magTotalBusiness;
        private readonly KpiManageRecordBusiness _magRecordBusiness;
        private readonly KpiManageDetailBusiness _magDetailBusiness;
        private readonly KpiManageAuditRecordBusiness _magAuditRecordBusiness;
        public KpiEvaluationWrappers(
            KpiTemplateBusiness tplBusiness,
            KpiTemplateRecordBusiness tplRecordBusiness,
            KpiManageTotalBusiness magTotalBusiness,
            KpiManageRecordBusiness magRecordBusiness,
            KpiManageDetailBusiness magDetailBusiness,
            KpiManageAuditRecordBusiness magAuditRecordBusiness
            )
        {
            _tplRecordBusiness = tplRecordBusiness;
            _tplBusiness = tplBusiness;

            _magTotalBusiness = magTotalBusiness;
            _magRecordBusiness = magRecordBusiness;
            _magDetailBusiness = magDetailBusiness;
            _magAuditRecordBusiness = magAuditRecordBusiness;
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
            foreach (var item in modelDto.TemplateRecord)
            {
                var lsContent = JsonConvert.DeserializeObject<List<KpiTemplateContentsDto>>(item.Contents).ToList();
                if (lsContent.Count() == 0)
                {
                    return Result.Fail("请设置考核内容");
                }
                var lsAudits = JsonConvert.DeserializeObject<List<KpiTemplateAuditsDto>>(item.Audits).ToList();
                if (lsAudits.Count() == 0)
                {
                    return Result.Fail("请设置审核人");
                }
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

                    var whereTplRecord = new Condition<KpiTemplateRecord>();
                    whereTplRecord.And(p => p.KpiType == modelDto.KpiType);
                    whereTplRecord.And(p => p.KpiId == modelDto.KpiId);

                    if (modelDto.KpiType == KpiType.Dept) //部门
                    {
                        var dptIds = modelDto.TemplateRecord.Select(p => p.DptId);
                        whereTplRecord.And(p => dptIds.Contains(p.DptId));
                        dbTplRecord = _tplRecordBusiness.Query(whereTplRecord.Combine()).ToList();
                        dbTpl = _tplBusiness.Query(p => p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                    }
                    else //人员
                    {
                        var employeeIds = modelDto.TemplateRecord.Select(p => p.EmployeeId);
                        whereTplRecord.And(p => employeeIds.Contains(p.EmployeeId));
                        dbTplRecord = _tplRecordBusiness.Query(whereTplRecord.Combine()).ToList();
                        dbTpl = _tplBusiness.Query(p => p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();
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
                            var modelTemp = distinct[key];
                            modelTemp.Contents = item.Contents;
                            modelTemp.Audits = item.Audits;
                            lsExistTplRecord.Add(modelTemp);
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

                    //考核模板
                    var lsExistTpl = new List<KpiTemplate>();
                    var lsNotExistTpl = new List<KpiTemplate>();

                    foreach (var tplRecord in lsTplRecord)
                    {
                        //考核模板
                        KpiTemplate modelTpl = null;
                        if (dbTpl.Count > 0)
                        {
                            if (modelDto.KpiType == KpiType.Dept) //部门
                            {
                                modelTpl = dbTpl.FirstOrDefault(p => p.CompanyId == tplRecord.CompanyId && p.DptId == tplRecord.DptId);
                            }
                            else //人员
                            {
                                modelTpl = dbTpl.FirstOrDefault(p => p.CompanyId == tplRecord.CompanyId && p.DptId == tplRecord.DptId && p.EmployeeId == tplRecord.EmployeeId);
                            }
                        }
                        if (modelTpl == null)
                        {
                            lsNotExistTpl.Add(new KpiTemplate()
                            {
                                Id = 0,
                                KpiType = tplRecord.KpiType,
                                CompanyId = tplRecord.CompanyId,
                                CompanyName = tplRecord.CompanyName,
                                DptId = tplRecord.DptId,
                                DptName = tplRecord.DptName,
                                EmployeeId = tplRecord.EmployeeId,
                                UserName = tplRecord.UserName,
                                Monthly = modelDto.KpiId == KpiPlan.Monthly ? tplRecord.Id : 0,
                                Quarter = modelDto.KpiId == KpiPlan.Quarter ? tplRecord.Id : 0,
                                HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? tplRecord.Id : 0,
                                Annual = modelDto.KpiId == KpiPlan.Annual ? tplRecord.Id : 0,
                            });
                        }
                        else
                        {
                            modelTpl.Monthly = modelDto.KpiId == KpiPlan.Monthly ? tplRecord.Id : modelTpl.Monthly;
                            modelTpl.Quarter = modelDto.KpiId == KpiPlan.Quarter ? tplRecord.Id : modelTpl.Quarter;
                            modelTpl.HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? tplRecord.Id : modelTpl.HalfYear;
                            modelTpl.Annual = modelDto.KpiId == KpiPlan.Annual ? tplRecord.Id : modelTpl.Annual;
                            lsExistTpl.Add(modelTpl);
                        }
                    }

                    if (lsNotExistTplRecord.Count > 0)
                    {
                        _tplRecordBusiness.UpdateRange(lsExistTplRecord);
                    }
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
                return Result.Fail("操作失败：" + ex.Message);
            }
        }


        /// <summary>
        /// [考核提交] 考核管理
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        public Result EditManage(KpiEvaluationManageSubmitDto modelDto)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var dtNow = DateTime.Now;
                    if (modelDto.ManageRecord == null || modelDto.ManageDetails.Count <= 0)
                    {
                        return Result.Fail("传入参数错误");
                    }
                    var fromMagRecord = modelDto.ManageRecord;
                    //考核管理记录
                    var modelMagRecord = _magRecordBusiness.GetSingle(p => p.Id == fromMagRecord.Id);
                    if (modelMagRecord == null)
                    {
                        return Result.Fail("未查询到相关记录");
                    }
                    if (modelMagRecord.Status == KpiStatus.Complete || modelMagRecord.Status == KpiStatus.Invalid)
                    {
                        return Result.Fail("该记录已被处理，请勿重复操作");
                    }

                    //当前处理人
                    var employee = modelDto.Employee;
                    //下一步处理人
                    var nextEmployee = modelDto.NextEmployee;
                    if ((modelMagRecord.Steps == KpiSteps.Zero || modelMagRecord.Steps == KpiSteps.One) && nextEmployee == null)
                    {
                        return Result.Fail("下一步处理人信息错误，请检查");
                    }

                    if (modelMagRecord.StepsCompanyId != employee.CompanyId || modelMagRecord.StepsDptId != employee.DptId || modelMagRecord.StepsEmployeeId != employee.Id)
                    {
                        return Result.Fail("请勿处理非本人处理的数据");
                    }

                    //考核管理统计
                    var modelMagTotal = _magTotalBusiness.GetSingle(p => p.KpiType == modelMagRecord.KpiType && p.KpiId == modelMagRecord.KpiId && p.CompanyId == modelMagRecord.CompanyId && p.DptId == modelMagRecord.DptId && p.EmployeeId == modelMagRecord.EmployeeId);
                    if (modelMagTotal == null)
                    {
                        return Result.Fail("未查询到相关记录：total");
                    }

                    //考核管理明细
                    var lsMagDetail = new List<KpiManageDetail>();
                    decimal selfScore = 0, oneScore = 0, twoScore = 0;
                    foreach (var item in modelDto.ManageDetails)
                    {
                        item.KpiManageRecordId = modelMagRecord.Id;
                        if (modelMagRecord.Steps == KpiSteps.Zero && item.SelfScore > item.Weight)
                        {
                            return Result.Fail($"[{item.EvaluationName}]项的自评分不能大于权重(分值)");
                        }
                        if (modelMagRecord.Steps == KpiSteps.One && item.OneScore > item.Weight)
                        {
                            return Result.Fail($"[{item.EvaluationName}]项的初审分不能大于权重(分值)");
                        }
                        if (modelMagRecord.Steps == KpiSteps.Two && item.TwoScore > item.Weight)
                        {
                            return Result.Fail($"[{item.EvaluationName}]项的终审分不能大于权重(分值)");
                        }
                        selfScore += item.SelfScore.Value;
                        oneScore += item.OneScore.Value;
                        twoScore += item.TwoScore.Value;
                    }

                    if (nextEmployee != null)
                    {
                        modelMagRecord.StepsCompanyId = nextEmployee.CompanyId;
                        modelMagRecord.StepsCompanyName = nextEmployee.CompanyName;
                        modelMagRecord.StepsDptId = nextEmployee.DptId;
                        modelMagRecord.StepsDptName = nextEmployee.DptName;
                        modelMagRecord.StepsEmployeeId = nextEmployee.Id;
                        modelMagRecord.StepsUserName = nextEmployee.EmployeeName;
                    }

                    var kpiTime = int.Parse(modelMagRecord.KpiDate);

                    //自评
                    if (modelMagRecord.Steps == KpiSteps.Zero)
                    {
                        modelMagRecord.Steps = KpiSteps.One;
                        modelMagRecord.Status = KpiStatus.Audit;
                        modelMagRecord.Score = selfScore;
                        for (int i = 0; i < 12; i++)
                        {
                            if ((i + 1) == kpiTime)
                            {
                                DynaimcHelper.DynamicFileds[i].Score.SetValue(modelMagTotal, selfScore);
                                DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.Audit);
                            }
                        }
                    }
                    //初审
                    else if (modelMagRecord.Steps == KpiSteps.One)
                    {
                        modelMagRecord.Steps = KpiSteps.Two;
                        modelMagRecord.Status = KpiStatus.Audit;
                        modelMagRecord.Score = selfScore + oneScore;
                        for (int i = 0; i < 12; i++)
                        {
                            if ((i + 1) == kpiTime)
                            {
                                DynaimcHelper.DynamicFileds[i].Score.SetValue(modelMagTotal, selfScore + oneScore);
                                DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.Audit);
                            }
                        }
                    }
                    //终审
                    else if (modelMagRecord.Steps == KpiSteps.Two)
                    {
                        modelMagRecord.Steps = KpiSteps.Complete;
                        modelMagRecord.Status = KpiStatus.Complete;
                        modelMagRecord.CompleteDate = dtNow;
                        modelMagRecord.Score = selfScore + oneScore + twoScore;

                        modelMagRecord.StepsCompanyId = null;
                        modelMagRecord.StepsCompanyName = null;
                        modelMagRecord.StepsDptId = null;
                        modelMagRecord.StepsDptName = null;
                        modelMagRecord.StepsEmployeeId = null;
                        modelMagRecord.StepsUserName = null;

                        for (int i = 0; i < 12; i++)
                        {
                            if ((i + 1) == kpiTime)
                            {
                                DynaimcHelper.DynamicFileds[i].Score.SetValue(modelMagTotal, selfScore + oneScore + twoScore);
                                DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.Complete);
                            }
                        }
                    }

                    //考核管理审核记录
                    var modelMageAuditRecord = new KpiManageAuditRecord()
                    {
                        Id = 0,
                        KpiManageRecordId = modelMagRecord.Id,
                        Steps = fromMagRecord.Steps,
                        Evaluation = modelDto.Evaluation,
                        CompanyId = fromMagRecord.StepsCompanyId.Value,
                        CompanyName = fromMagRecord.StepsCompanyName,
                        DptId = fromMagRecord.StepsDptId.Value,
                        DptName = fromMagRecord.StepsDptName,
                        EmployeeId = fromMagRecord.StepsEmployeeId.Value,
                        UserName = fromMagRecord.StepsUserName,
                        AddDate = dtNow
                    };

                    _magTotalBusiness.Update(modelMagTotal);
                    _magRecordBusiness.Update(modelMagRecord);
                    _magDetailBusiness.UpdateRange(modelDto.ManageDetails);
                    _magAuditRecordBusiness.Add(modelMageAuditRecord);

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
        /// [生成] 考核记录
        /// </summary>
        /// <param name="kpiId">考核方案</param>
        /// <returns></returns>
        public Result GeneratedManage(KpiPlan kpiId)
        {
            try
            {
                var dtNow = DateTime.Now;
                var currTime = 0;
                var currYear = dtNow.Year;

                switch (kpiId)
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
                var kpiDate = currTime.ToString();

                using (TransactionScope ts = new TransactionScope())
                {
                    var dbTplRecord = _tplRecordBusiness.QueryByGenerated(kpiId, currYear, kpiDate);
                    if (dbTplRecord.Count <= 0)
                    {
                        return Result.Fail("没有需要生成的数据", "201");
                    }

                    //考核管理统计
                    var dbMagTotal = _magTotalBusiness.Query(p => p.KpiId == kpiId && p.Year == currYear);

                    //考核管理统计
                    var lsNotExistMagTotal = new List<KpiManageTotal>();
                    var lsExistMagTotal = new List<KpiManageTotal>();
                    //考核管理记录
                    var lsNotExistMagRecord = new List<KpiManageRecord>();
                    //考核管理明细
                    var lsNotExistMagDetail = new List<KpiManageDetail>();
                    foreach (var tplRecord in dbTplRecord)
                    {
                        #region 考核管理统计
                        KpiManageTotal modelMagTotal = null;
                        if (dbMagTotal.Count > 0)
                        {
                            modelMagTotal = dbMagTotal.FirstOrDefault(p => p.KpiType == tplRecord.KpiType && p.CompanyId == tplRecord.CompanyId && p.DptId == tplRecord.DptId && p.EmployeeId == tplRecord.EmployeeId);
                        }
                        if (modelMagTotal == null)
                        {
                            modelMagTotal = new KpiManageTotal()
                            {
                                Id = 0,
                                KpiType = tplRecord.KpiType,
                                KpiId = tplRecord.KpiId,
                                KpiName = tplRecord.KpiName,
                                CompanyId = tplRecord.CompanyId,
                                CompanyName = tplRecord.CompanyName,
                                DptId = tplRecord.DptId,
                                DptName = tplRecord.DptName,
                                EmployeeId = tplRecord.EmployeeId,
                                UserName = tplRecord.UserName,
                                Year = currYear
                            };
                            for (int i = 0; i < 12; i++)
                            {
                                DynaimcHelper.DynamicFileds[i].Score.SetValue(modelMagTotal, (decimal)0);
                                if ((i + 1) < currTime)
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.Invalid);
                                }
                                else if ((i + 1) == currTime)
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.Assess);
                                }
                                else
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.NotStarted);
                                }
                            }
                            lsNotExistMagTotal.Add(modelMagTotal);
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                if ((i + 1) == currTime)
                                {
                                    DynaimcHelper.DynamicFileds[i].Status.SetValue(modelMagTotal, KpiStatus.Assess);
                                }
                            }
                            lsExistMagTotal.Add(modelMagTotal);
                        }
                        #endregion

                        lsNotExistMagRecord.Add(new KpiManageRecord()
                        {
                            Id = 0,
                            KpiTemplateRecordId = tplRecord.Id,
                            KpiType = tplRecord.KpiType,
                            KpiId = tplRecord.KpiId,
                            KpiName = tplRecord.KpiName,
                            CompanyId = tplRecord.CompanyId,
                            CompanyName = tplRecord.CompanyName,
                            DptId = tplRecord.DptId,
                            DptName = tplRecord.DptName,
                            EmployeeId = tplRecord.EmployeeId,
                            UserName = tplRecord.UserName,
                            Year = currYear,
                            KpiDate = kpiDate,
                            Steps = KpiSteps.Zero,
                            StepsCompanyId = tplRecord.CompanyId,
                            StepsCompanyName = tplRecord.CompanyName,
                            StepsDptId = tplRecord.DptId,
                            StepsDptName = tplRecord.DptName,
                            StepsEmployeeId = tplRecord.EmployeeId,
                            StepsUserName = tplRecord.UserName,
                            AddDate = dtNow,
                            Score = 0,
                            Status = KpiStatus.Assess
                        });
                    }

                    _magRecordBusiness.AddRange(lsNotExistMagRecord);

                    foreach (var magTecord in lsNotExistMagRecord)
                    {
                        var modelTplRecord = dbTplRecord.FirstOrDefault(p => p.Id == magTecord.KpiTemplateRecordId);
                        if (modelTplRecord != null)
                        {
                            var lsTplContents = JsonConvert.DeserializeObject<List<KpiTemplateContentsDto>>(modelTplRecord.Contents).OrderBy(p => p.EvaluationId);
                            foreach (var tplContent in lsTplContents)
                            {
                                lsNotExistMagDetail.Add(new KpiManageDetail
                                {
                                    Id = 0,
                                    KpiManageRecordId = magTecord.Id,
                                    CompanyId = magTecord.CompanyId,
                                    DptId = magTecord.DptId,
                                    EmployeeId = magTecord.EmployeeId,
                                    EvaluationId = tplContent.EvaluationId,
                                    EvaluationName = tplContent.EvaluationName,
                                    EvaluationType = tplContent.EvaluationType,
                                    Weight = tplContent.Weight,
                                    Explain = tplContent.Explain,
                                    Year = magTecord.Year,
                                    KpiDate = magTecord.KpiDate,
                                    SelfScore = 0,
                                    OneScore = 0,
                                    TwoScore = 0
                                });
                            }
                        }
                    }
                    if (lsNotExistMagDetail.Count > 0)
                    {
                        _magDetailBusiness.AddRange(lsNotExistMagDetail);
                    }

                    _magTotalBusiness.AddRange(lsNotExistMagTotal);
                    if (lsExistMagTotal.Count > 0)
                    {
                        _magTotalBusiness.UpdateRange(lsExistMagTotal);
                    }

                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("失败：" + ex.Message);
            }
        }

    }
}
