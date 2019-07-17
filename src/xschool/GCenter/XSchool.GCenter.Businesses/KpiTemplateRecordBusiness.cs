using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;
using System.Linq;

namespace XSchool.GCenter.Businesses
{
    public class KpiTemplateRecordBusiness : Business<KpiTemplateRecord>
    {
        private readonly KpiTemplateRecordRepository _tplRecordRepository;
        private readonly KpiTemplateRepository _tplRepository;
        private readonly KpiTemplateDetailRepository _tplDetailRepository;
        private readonly KpiTemplateAuditRecordRepository _tplAuditRecordRepository;
        public KpiTemplateRecordBusiness(IServiceProvider provider,
            KpiTemplateRecordRepository tplRecordRepository,
            KpiTemplateRepository tplRepository,
            KpiTemplateDetailRepository tplDetailRepository,
            KpiTemplateAuditRecordRepository tplAuditRecordRepository) : base(provider, tplRecordRepository)
        {
            _tplRecordRepository = tplRecordRepository;
            _tplRepository = tplRepository;
            _tplDetailRepository = tplDetailRepository;
            _tplAuditRecordRepository = tplAuditRecordRepository;
        }

        /// <summary>
        /// 验证 传入的参数值
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

        public Result AddOrEdit(KpiEvaluationTemplatSubmitDto modelDto)
        {
            try
            {
                var result = CheckTemplat(modelDto);
                if (!result.Succeed)
                {
                    return result;
                }

                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
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
                        dbTplRecord = _tplRecordRepository.Query(whereTplRecord.Combine()).ToList();
                        dbTpl = _tplRepository.Query(p => p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                    }
                    else //人员
                    {
                        var employeeIds = modelDto.TemplateRecord.Select(p => p.EmployeeId);
                        whereTplRecord.And(p => employeeIds.Contains(p.EmployeeId));
                        dbTplRecord = _tplRecordRepository.Query(whereTplRecord.Combine()).ToList();
                        dbTpl = _tplRepository.Query(p => p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();
                    }

                    var lsExistTplRecord = new List<KpiTemplateRecord>();
                    var lsNotExistTplRecord = new List<KpiTemplateRecord>();

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
                        _tplRecordRepository.AddRange(lsNotExistTplRecord);
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
                        if (modelDto.KpiType == KpiType.Dept) //部门
                        {
                            modelTpl = dbTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId);
                        }
                        else //人员
                        {
                            modelTpl = dbTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId && p.EmployeeId == record.EmployeeId);
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
                    }

                    if (lsExistTplRecord.Count > 0)
                    {
                        var templateRecordIds = lsExistTplRecord.Select(p => p.Id);
                        _tplDetailRepository.Delete(p => templateRecordIds.Contains(p.KpiTemplateRecordId));
                        _tplAuditRecordRepository.Delete(p => templateRecordIds.Contains(p.KpiTemplateRecordId));
                    }

                    _tplDetailRepository.AddRange(lsTplDetail);
                    _tplAuditRecordRepository.AddRange(lsTplAuditRecord);
                    if (lsNotExistTpl.Count > 0)
                    {
                        _tplRepository.AddRange(lsNotExistTpl);
                    }
                    if (lsExistTpl.Count > 0)
                    {
                        _tplRepository.UpdateRange(lsExistTpl);
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
    }
}
